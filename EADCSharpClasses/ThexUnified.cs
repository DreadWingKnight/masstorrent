/*
 * 
 * Tiger Tree Hash Threaded - by Gil Schmidt.
 * 
 *  - this code was writtin based on:
 *    "Tree Hash EXchange format (THEX)" 
 *    http://www.open-content.net/specs/draft-jchapweske-thex-02.html
 * 
 *  - the tiger hash class was converted from visual basic code called TigerNet:
 *    http://www.hotpixel.net/software.html
 * 
 *  - Base32 class was taken from:
 *    http://msdn.microsoft.com/msdnmag/issues/04/07/CustomPreferences/default.aspx
 *    didn't want to waste my time on writing a Base32 class.
 * 
 *  - along with the request for a version the return the full TTH tree and the need
 *    for a faster version i rewrote a thread base version of the ThexCS.
 *    i must say that the outcome wasn't as good as i thought it would be.
 *    after writing ThexOptimized i noticed that the major "speed barrier"
 *    was reading the data from the file so i decide to split it into threads
 *    that each one will read the data and will make the computing process shorter.
 *    in testing i found out that small files (about 50 mb) are being processed 
 *    faster but in big files (700 mb) it was slower, also the CPU is working better
 *    with more threads.
 *    
 *  - the update for the ThexThreaded is now including a Dispose() function to free
 *    some memory which is mostly taken by the TTH array, also i changed the way the
 *    data is pulled out of the file so it would read data block instead of data leaf 
 *    every time (reduced the i/o reads dramaticly and go easy on the hd) i used 1 MB
 *    blocks for each thread you can set it at DataBlockSize (put something like: 
 *    LeafSize * N). the method for copying bytes is change to Buffer.BlockCopy it's
 *    faster but you won't notice it too much. 
 * 
 *    (a lot of threads = slower but the cpu is working less, i recommend 3-5 threads)
 * 
 * 
 *  [ contact me at Gil_Smdt@hotmali.com ]
 */

using System;
using System.Threading;
using System.IO;
using System.Collections;

namespace EAD.Cryptography.ThexCS
{
	public class Thex
	{
		const   int        Block_Size = 1024;
		private int        Leaf_Count;
		private ArrayList  LeafCollection;
		private FileStream FilePtr;

		private struct HashHolder
		{
			public byte[] HashValue;

			public HashHolder(byte[] HashValue)
			{
				this.HashValue = HashValue;
			}
		}

		public byte[] GetTTH(string Filename) 
		{
			HashHolder Result;

			try
			{
				FilePtr = new FileStream(Filename,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
			
				//if there's only one block in file use SmallFile().
				if (FilePtr.Length <= Block_Size) return SmallFile();

				//get how many leafs are in file.
				Leaf_Count = (int) FilePtr.Length / Block_Size;
				if (FilePtr.Length % Block_Size > 0) Leaf_Count++;
			
				//load blocks of data and get tiger hash for each one.
				LoadLeafHash();
			
				//get root hash from blocks hash.
				Result = GetRootHash();

				return Result.HashValue;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine("error while trying to get TTH for file: " + 
					Filename + ". (" + e.Message + ")");
				return null;
			}
		}

		private byte[] SmallFile()
		{
			Tiger TG = new Tiger();
			byte[] Block = new byte[Block_Size];

			int BlockSize = FilePtr.Read(Block,0,1024);
			FilePtr.Close();

			//gets hash for a single block file.
			return LH(ByteExtract(Block,BlockSize));
		}

		private void LoadLeafHash()
		{
			LeafCollection = new ArrayList();

			for (int i = 0; i < (int) Leaf_Count / 2; i++)
			{
				byte[] BlockA = new byte[Block_Size],BlockB = new byte[Block_Size];
				
				FilePtr.Read(BlockA,0,1024);
				int DataSize = FilePtr.Read(BlockB,0,1024);

				//check if the block isn't big enough.
				if (DataSize < Block_Size)
					BlockB = ByteExtract(BlockB,DataSize);

				BlockA = LH(BlockA);
				BlockB = LH(BlockB);

				//add combined leaf hash.
				LeafCollection.Add(new HashHolder(IH(BlockA,BlockB)));
			}

			//leaf without a pair.
			if (Leaf_Count % 2 != 0)
			{
				byte[] Block = new byte[Block_Size];
				int DataSize = FilePtr.Read(Block,0,1024);

				if (DataSize < 1024)
					Block = ByteExtract(Block,DataSize);

				LeafCollection.Add(new HashHolder(LH(Block)));
			}

			FilePtr.Close();
		}

		private HashHolder GetRootHash()
		{
			ArrayList InternalCollection = new ArrayList();

			do
			{
				InternalCollection = new ArrayList(LeafCollection);
				LeafCollection.Clear();

			while (InternalCollection.Count > 1)
			{
				//load next two leafs.
				byte[] HashA = ((HashHolder) InternalCollection[0]).HashValue;
				byte[] HashB = ((HashHolder) InternalCollection[1]).HashValue;
					
				//add their combined hash.
				LeafCollection.Add(new HashHolder(IH(HashA,HashB)));

				//remove the used leafs.
				InternalCollection.RemoveAt(0);
				InternalCollection.RemoveAt(0);
			}

				//if this leaf can't combine add him at the end.
				if (InternalCollection.Count > 0)
					LeafCollection.Add(InternalCollection[0]);
			} while (LeafCollection.Count > 1);

			return (HashHolder) LeafCollection[0];
		}

		private byte[] IH(byte[] LeafA,byte[] LeafB) //internal hash.
		{ 
			byte[] Data = new byte[LeafA.Length + LeafB.Length + 1];

			Data[0] = 0x01; //internal hash mark.

			//combines two leafs.
			LeafA.CopyTo(Data,1);
			LeafB.CopyTo(Data,LeafA.Length + 1);

			//gets tiger hash value for combined leaf hash.
			Tiger TG = new Tiger();
			TG.Initialize();
			return TG.ComputeHash(Data);			
		}

		private byte[] LH(byte[] Raw_Data) //leaf hash.
		{ 
			byte[] Data = new byte[Raw_Data.Length + 1];

			Data[0] = 0x00; //leaf hash mark.
			Raw_Data.CopyTo(Data,1);

			//gets tiger hash value for leafs blocks.
			Tiger TG = new Tiger();
			TG.Initialize();
			return TG.ComputeHash(Data);
		}

		private byte[] ByteExtract(byte[] Raw_Data,int Data_Length) //copy 
		{
			//return Data_Length bytes from Raw_Data.
			byte[] Data = new byte[Data_Length];

			for (int i = 0; i < Data_Length; i++)
				Data[i] = Raw_Data[i];

			return Data;
		}
	}

	public class ThexOptimized
	{
		const   int        Block_Size      = 128;   //64k 
		const   int        Leaf_Size       = 1024; //1k - don't change this.

		private int        Leaf_Count; //number of leafs.
		private byte[][]   HashValues; //array for hash values.
		private FileStream FilePtr;    //dest file stream pointer.

		public byte[] GetTTH(string Filename) 
		{
			try
			{
				byte[] TTH = null;

				if (!File.Exists(Filename)) 
				{
					System.Diagnostics.Debug.WriteLine("file doesn't exists " + Filename);
					return null;
				}

				//open the file.
				FilePtr = new FileStream(Filename,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
			
				//the file is smaller then a single leaf.
				if (FilePtr.Length <= Leaf_Size * Block_Size)
					TTH = CompressSmallBlock();
				else
				{
					Leaf_Count = (int) FilePtr.Length / Leaf_Size;
					if (FilePtr.Length % Leaf_Size > 0) Leaf_Count++;
					
					GetLeafHash(); //get leafs hash from file.
					System.Diagnostics.Debug.WriteLine("===> [ Moving to internal hash. ]");
					TTH = CompressHashBlock(HashValues,Leaf_Count);	//get root TTH from hash array.
				}
				return TTH;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine("error while trying to get TTH for file: " + 
					Filename + ". (" + e.Message + ")");
				
				FilePtr.Close();
				return null;
			}
		}

		private void GetLeafHash() //gets the leafs from the file and hashes them.
		{
			int i;
			int Blocks_Count = (int) Leaf_Count / (Block_Size * 2);
			
			if (Leaf_Count % (Block_Size * 2) > 0) Blocks_Count++;
			HashValues = new byte[Blocks_Count][];

			byte[][] HashBlock = new byte[(int)Block_Size][];
			
			for (i = 0; i < (int) Leaf_Count / (Block_Size * 2); i++) //loops threw the blocks.
			{
				for (int LeafIndex = 0; LeafIndex < (int) Block_Size; LeafIndex++) //creates new block.
					HashBlock[LeafIndex] = GetNextLeafHash(); //extracts two leafs to a hash.

				HashValues[i] = CompressHashBlock(HashBlock,Block_Size); //compresses the block to hash.
			}

			if (i < Blocks_Count) HashValues[i] = CompressSmallBlock(); //this block wasn't big enough.
			
			Leaf_Count = Blocks_Count;
			FilePtr.Close();
		}

		private byte[] GetNextLeafHash() //reads 2 leafs from the file and returns their combined hash.
		{
			byte[] LeafA = new byte[Leaf_Size];
			byte[] LeafB = new byte[Leaf_Size];
				
			
			int DataSize = FilePtr.Read(LeafA,0,Leaf_Size);
			//check if leaf is too small.
			if (DataSize < Leaf_Size) return (LeafHash(ByteExtract(LeafA,DataSize)));

			DataSize = FilePtr.Read(LeafB,0,Leaf_Size);

			if (DataSize < Leaf_Size)
				LeafB = ByteExtract(LeafB,DataSize);

			LeafA = LeafHash(LeafA);
			LeafB = LeafHash(LeafB);

			return InternalHash(LeafA,LeafB); //returns combined hash.
		}

		private byte[] CompressHashBlock(byte[][] HashBlock,int HashCount) //compresses hash blocks to hash.
		{
			if (HashBlock.Length == 0) return null;

			while (HashCount > 1) //until there's only 1 hash.
			{
				int TempBlockSize = (int) HashCount / 2;
				if (HashCount % 2 > 0) TempBlockSize++;

				byte[][] TempBlock = new byte[TempBlockSize][];

				int HashIndex = 0;
				for (int i = 0; i < (int) HashCount / 2; i++) //makes hash from pairs.
				{
					TempBlock[i] = InternalHash(HashBlock[HashIndex],HashBlock[HashIndex+1]);
					HashIndex += 2;
				}
			
				//this one doesn't have a pair :(
				if (HashCount % 2 > 0) TempBlock[TempBlockSize - 1] = HashBlock[HashCount - 1];

				HashBlock = TempBlock;
				HashCount = TempBlockSize;
			}
			return HashBlock[0];
		}

		private byte[] CompressSmallBlock() //compress a small block to hash.
		{
			long DataSize = (long) (FilePtr.Length - FilePtr.Position);
			
			int LeafCount = (int) DataSize / (Leaf_Size * 2);
			if (DataSize % (Leaf_Size * 2) > 0) LeafCount++;

			byte[][] SmallBlock = new byte[LeafCount][];		

			//extracts leafs from file.
			for (int i = 0; i < (int) DataSize / (Leaf_Size * 2); i++)
				SmallBlock[i] = GetNextLeafHash();

			if (DataSize % (Leaf_Size * 2) > 0) SmallBlock[LeafCount - 1] = GetNextLeafHash();

			FilePtr.Close();
			return CompressHashBlock(SmallBlock,LeafCount); //gets hash from the small block.
		}
		private byte[] InternalHash(byte[] LeafA,byte[] LeafB) //internal hash.
		{ 
			byte[] Data = new byte[LeafA.Length + LeafB.Length + 1];

			Data[0] = 0x01; //internal hash mark.

			//combines two leafs.
			LeafA.CopyTo(Data,1);
			LeafB.CopyTo(Data,LeafA.Length + 1);

			//gets tiger hash value for combined leaf hash.
			Tiger TG = new Tiger();
			TG.Initialize();
			return TG.ComputeHash(Data);			
		}

		private byte[] LeafHash(byte[] Raw_Data) //leaf hash.
		{ 
			byte[] Data = new byte[Raw_Data.Length + 1];

			Data[0] = 0x00; //leaf hash mark.
			Raw_Data.CopyTo(Data,1);

			//gets tiger hash value for leafs blocks.
			Tiger TG = new Tiger();
			TG.Initialize();
			return TG.ComputeHash(Data);
		}

		private byte[] ByteExtract(byte[] Raw_Data,int Data_Length) //if we use the extra 0x00 in Raw_Data we will get wrong hash.
		{
			byte[] Data = new byte[Data_Length];

			for (int i = 0; i < Data_Length; i++)
				Data[i] = Raw_Data[i];

			return Data;
		}
	}


	public class ThexThreaded
	{
		const byte LeafHash = 0x00;
		const byte InternalHash = 0x01;
		const int  LeafSize = 1024;
		const int  DataBlockSize = LeafSize * 1024; // 1 MB
		const int  ThreadCount = 4;

		public byte[][][] TTH;
		public int LevelCount;
		string Filename;
		int LeafCount;
		FileStream FilePtr;

		FileBlock[] FileParts = new FileBlock[ThreadCount];
		Thread[] ThreadsList = new Thread[ThreadCount];

		public byte[] GetTTH_Value(string Filename)
		{
			GetTTH(Filename);
			return TTH[LevelCount-1][0];
		}

		public byte[][][] GetTTH_Tree(string Filename)
		{
			GetTTH(Filename);
			return TTH;			
		}

		private void GetTTH(string Filename)
		{
			this.Filename = Filename;

			//			try
			//			{
			OpenFile();
			Initialize();
			SplitFile();
			Console.WriteLine("starting to get TTH: " + DateTime.Now.ToString());
			StartThreads();
			Console.WriteLine("finished to get TTH: " + DateTime.Now.ToString());
			GC.Collect();
			CompressTree();
			/*			}
						catch (Exception e)
						{
							Console.WriteLine("error while trying to get TTH: " + e.Message);
							StopThreads();				
						}*/

			if (FilePtr != null) FilePtr.Close();
		}
		
		void Dispose()
		{
			TTH = null;
			ThreadsList = null;
			FileParts = null;
			GC.Collect();
		}

		void OpenFile()
		{
			if (!File.Exists(Filename))
				throw new Exception("file doesn't exists!");

			FilePtr = new FileStream(Filename,FileMode.Open,FileAccess.Read,FileShare.Read);
		}

		void Initialize()
		{
			int i = 1;
			LevelCount = 1;

			LeafCount = (int) (FilePtr.Length / LeafSize);
			if ((FilePtr.Length % LeafSize) > 0) LeafCount++;

			while (i < LeafCount) { i *= 2; LevelCount++; }

			TTH = new byte[LevelCount][][];
			TTH[0] = new byte[LeafCount][];
		}

		void SplitFile()
		{
			long LeafsInPart = LeafCount / ThreadCount;

			// check if file is bigger then 1 MB or don't use threads
			if (FilePtr.Length > 1024 * 1024) 
				for (int i = 0; i < ThreadCount; i++)
					FileParts[i] = new FileBlock(LeafsInPart * LeafSize * i,
						LeafsInPart * LeafSize * (i + 1));

			FileParts[ThreadCount - 1].End = FilePtr.Length;
		}

		void StartThreads()
		{
			for (int i = 0; i < ThreadCount; i++)
			{
				ThreadsList[i] = new Thread(new ThreadStart(ProcessLeafs));
				ThreadsList[i].IsBackground = true;
				ThreadsList[i].Name = i.ToString();
				ThreadsList[i].Start();
			}

			bool ThreadsAreWorking = false;
			
			do 
			{
				Thread.Sleep(1000);
				ThreadsAreWorking = false;

				for (int i = 0; i < ThreadCount; i++)
					if (ThreadsList[i].IsAlive)
						ThreadsAreWorking = true;


			} while (ThreadsAreWorking);
		}

		void StopThreads()
		{
			for (int i = 0; i < ThreadCount; i++)
				if (ThreadsList[i] != null && ThreadsList[i].IsAlive) 
					ThreadsList[i].Abort();
		}

		void ProcessLeafs()
		{
			FileStream ThreadFilePtr = new FileStream(Filename,FileMode.Open,FileAccess.Read);
			FileBlock ThreadFileBlock = FileParts[Convert.ToInt16(Thread.CurrentThread.Name)];
			Tiger TG = new Tiger();
			byte[] DataBlock;
			byte[] Data = new byte[LeafSize + 1];
			int LeafIndex,BlockLeafs;
			int i;

			ThreadFilePtr.Position = ThreadFileBlock.Start;
			
			while (ThreadFilePtr.Position < ThreadFileBlock.End)
			{
				LeafIndex = (int) ThreadFilePtr.Position / 1024;
				
				if (ThreadFileBlock.End - ThreadFilePtr.Position < DataBlockSize)
					DataBlock = new byte[ThreadFileBlock.End - ThreadFilePtr.Position];
				else
					DataBlock = new byte[DataBlockSize];

				ThreadFilePtr.Read(DataBlock,0,DataBlock.Length); //read block

				BlockLeafs = DataBlock.Length / 1024;
						
				for (i = 0; i < BlockLeafs; i++)
				{
					Buffer.BlockCopy(DataBlock,i * LeafSize,Data,1,LeafSize);

					TG.Initialize();
					TTH[0][LeafIndex++] = TG.ComputeHash(Data);
				}

				if (i * LeafSize < DataBlock.Length)
				{
					Data = new byte[DataBlock.Length - BlockLeafs * LeafSize + 1];
					Data[0] = LeafHash;

					Buffer.BlockCopy(DataBlock,BlockLeafs * LeafSize,Data,1,(Data.Length - 1));

					TG.Initialize();
					TTH[0][LeafIndex++] = TG.ComputeHash(Data);

					Data = new byte[LeafSize + 1];
					Data[0] = LeafHash;
				}
			}

			DataBlock = null;
			Data = null;
		}

		void CompressTree()
		{
			int InternalLeafCount;
			int Level = 0,i,LeafIndex;

			while (Level + 1 < LevelCount)
			{
				LeafIndex = 0;
				InternalLeafCount = (LeafCount / 2) + (LeafCount % 2);
				TTH[Level + 1] = new byte[InternalLeafCount][];

				for (i = 1; i < LeafCount; i += 2)
					ProcessInternalLeaf(Level + 1,LeafIndex++,TTH[Level][i - 1],TTH[Level][i]);

				if (LeafIndex < InternalLeafCount) 
					TTH[Level + 1][LeafIndex] = TTH[Level][LeafCount - 1];

				Level++;
				LeafCount = InternalLeafCount;
			}
		}

		void ProcessInternalLeaf(int Level,int Index,byte[] LeafA,byte[] LeafB)
		{
			Tiger TG = new Tiger();
			byte[] Data = new byte[LeafA.Length + LeafB.Length + 1];

			Data[0] = InternalHash;

			Buffer.BlockCopy(LeafA,0,Data,1,LeafA.Length);
			Buffer.BlockCopy(LeafB,0,Data,LeafA.Length + 1,LeafA.Length);

			TG.Initialize();
			TTH[Level][Index] = TG.ComputeHash(Data);
		}
	}

	struct FileBlock
	{
		public long Start,End;

		public FileBlock(long Start,long End)
		{
			this.Start = Start;
			this.End = End;
		}
	}
}
