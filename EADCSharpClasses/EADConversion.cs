/*
 * Enlist-a-distro Common C# classes
 * CoreHash Class
 * Constants Class
 * Hash display type Conversion
 * Byte Array - Raw - Hexadecimal
 * Code by Harold Feit - Depthstrike.com/Enlist-a-Distro
 * ToDo:
 * Add directional support for Base32 to:
 *  EAD.Conversion.EADCoreHash
 *  EAD.Conversion.HashChanger
 */

using System.Diagnostics;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Collections;
using System.Text;

namespace EAD
	{
		namespace CSharp
		{
			public class Constants
			{
				// Constants for directions for conversion (source outputs to non-source)
				public const int SourceBytes = 1;
				public const int SourceBinary = 2;
				public const int SourceHex = 3;
				public const int SourceBase32 = 4;

				// Constants for common torrent sections
				public const string AnnounceList = "announce-list";
				public const string WebSeeds = "httpseeds";

				// Constants for file and block sizes
				public const long ED2KBlockSize = 9728000;
			}
		}
		
		namespace Conversion
		{

			public class HashChanger
			{
				// Internally stored hash values
				private string rawhashvalue;
				private string hexhashvalue;
				private byte[] bytehashvalue;
				private string base32value;

				// Byte-Array hash value
				public byte[] bytehash
				{
					get
					{
						return bytehashvalue;
					}
					set
					{
						bytehashvalue = value;
						hexhashvalue = "";
						rawhashvalue = "";
						StringBuilder buff = new StringBuilder();
						rawhashvalue = System.Text.Encoding.Default.GetString(bytehashvalue);
						foreach (byte HashByte in bytehashvalue)
						{
							buff.AppendFormat("{0:x2}", HashByte);
						}
						hexhashvalue = buff.ToString();
						base32value = EAD.Conversion.Base32.ToBase32String(bytehashvalue);
					}
				}

				// Raw text hash value
				public string rawhash
				{
					get
					{
						return rawhashvalue;
					}
					set
					{
						rawhashvalue = value;
						bytehashvalue = System.Text.Encoding.Default.GetBytes(value);;
						StringBuilder buff = new StringBuilder();
						foreach (byte HashByte in bytehashvalue)
						{
							buff.AppendFormat("{0:x2}", HashByte);
						}
						hexhashvalue = buff.ToString();
						base32value = EAD.Conversion.Base32.ToBase32String(bytehashvalue);
					}
				}

				// Hexadecimal hash value
				public string hexhash
				{
					get
					{
						return hexhashvalue;
					}
					set
					{
						hexhashvalue = value;
						int spot;
						bytehashvalue = HexEncoding.GetBytes(hexhashvalue,out spot);
						rawhashvalue = System.Text.Encoding.Default.GetString(bytehashvalue);
						base32value = EAD.Conversion.Base32.ToBase32String(bytehashvalue);
					}
				}

				// Base32 hash value
				public string base32
				{
					get
					{
						return base32value;
					}
				}

			}
			
			public class EADCoreHash : EAD.CSharp.Constants
			{
							
				public bool HashConvert(ref byte[] hashbytes, ref string Binary, ref string hexadecimal, int source)
				{
					bool returnValue;
					returnValue = false;
					StringBuilder buff = new StringBuilder();
					
					if (source == SourceBytes)
					{
						Binary = "";
						hexadecimal = "";
						foreach (byte HashByte in hashbytes)
						{
							Binary = Binary + HashByte.ToString();
							buff.AppendFormat("{0:x2}", HashByte);
						}
						hexadecimal = buff.ToString();
						returnValue = true;
					}
					else if (source == SourceBinary)
					{
						hexadecimal = "";
						hashbytes = System.Text.Encoding.Default.GetBytes(Binary);;
						foreach (byte HashByte in hashbytes)
						{
							buff.AppendFormat("{0:x2}", HashByte);
						}
						hexadecimal = buff.ToString();
						returnValue = true;
					}
					else if (source == SourceHex)
					{
						Binary = "";
						int spot;
						hashbytes = HexEncoding.GetBytes(hexadecimal,out spot);
						foreach (byte HashByte in hashbytes)
						{
							Binary = Binary + HashByte.ToString();
						}
						returnValue = true;
					}
					return returnValue;
				}
			}
		}
	}