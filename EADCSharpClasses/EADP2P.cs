using System.Diagnostics;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Collections;
using System.Text;

namespace EAD
{
	namespace PeerToPeer
	{
		public class LinkGeneration
		{
			private string ED2KHexHash;
			private string SHA1Base32Hash;
			private string TigerTreeRootBase32Hash;
			private string LinkedFilename;
			private long LinkedFileSize;
			private const string MagnetSHA1Prefix = "magnet:?xt=urn:sha1:";
			private const string MagnetBitPrintPrefix = "magnet:?xt=urn:bitprint:";
			private const string MagnetED2KPrefix = "magnet:?xt=urn:ed2khash:";
			private const string MagnetED2KMidfix = "&xt=urn:ed2khash:";
			private const string ED2KPrefix = "ed2k://|file|";
			private byte[] convertbytes;
			private string converthex;
			private string convertbinary;
			private bool success;
			private EAD.Conversion.EADCoreHash convertme = new EAD.Conversion.EADCoreHash();
			public string SHA1Hex
			{
				set
				{
					converthex = value;
					success = convertme.HashConvert(ref convertbytes, ref convertbinary,ref converthex,3);
					SHA1Base32Hash = EAD.Conversion.Base32.ToBase32String(convertbytes);
					converthex = "";
					convertbinary = "";
					convertbytes = null;
				}
			}
			public string TigerHex
			{
				set
				{
					converthex = value;
					success = convertme.HashConvert(ref convertbytes, ref convertbinary,ref converthex,3);
					TigerTreeRootBase32Hash = EAD.Conversion.Base32.ToBase32String(convertbytes);
					converthex = "";
					convertbinary = "";
					convertbytes = null;
				}
			}
			public string ED2KRaw
			{
				set
				{
					convertbinary = value;
					success = convertme.HashConvert(ref convertbytes,ref convertbinary,ref converthex,2);
					ED2KHexHash = converthex;
					converthex = "";
					convertbinary = "";
					convertbytes = null;
				}
			}
			public string SHA1Raw
			{
				set
				{
					convertbinary = value;
					success = convertme.HashConvert(ref convertbytes,ref convertbinary,ref converthex,2);
					SHA1Base32Hash = EAD.Conversion.Base32.ToBase32String(convertbytes);
					converthex = "";
					convertbinary = "";
					convertbytes = null;
				}
			}
			public string TigerRaw
			{
				set
				{
					convertbinary = value;
					success = convertme.HashConvert(ref convertbytes,ref convertbinary,ref converthex,2);
					TigerTreeRootBase32Hash = EAD.Conversion.Base32.ToBase32String(convertbytes);
					converthex = "";
					convertbinary = "";
					convertbytes = null;
				}
			}
            public byte[] ed2kbytes
			{
				set
				{
					StringBuilder buff = new StringBuilder();
					buff = new StringBuilder();
					foreach (byte hashByte in value)
					{
						buff.AppendFormat("{0:x2}", hashByte);
					}
					ED2KHexHash = buff.ToString();
				}
			}
			public byte[] SHA1Bytes
			{
				set
				{
					SHA1Base32Hash = EAD.Conversion.Base32.ToBase32String(value);
				}
			}
			public byte[] TigerBytes
			{
				set
				{
					TigerTreeRootBase32Hash = EAD.Conversion.Base32.ToBase32String(value);
				}
			}
			public string ED2KHash
			{
				get
				{
					return ED2KHexHash;
				}
				set
				{
					ED2KHexHash = value;
				}
			}
			public string SHA1Hash
			{
				get
				{
					return SHA1Base32Hash;
				}
				set
				{
					SHA1Base32Hash = value;
				}
			}
			public string TTH
			{
				get
				{
					return TigerTreeRootBase32Hash;
				}
				set
				{
					TigerTreeRootBase32Hash = value;
				}
			}
			public string FileName
			{
				get
				{
					return LinkedFilename;
				}
				set
				{
					LinkedFilename = System.Web.HttpUtility.UrlDecode(value);
				}
			}
			public long FileSize
			{
				get
				{
					return LinkedFileSize;
				}
				set
				{
					LinkedFileSize = value;
				}
			}
			public string MagnetBitPrintHybrid
			{
				get
				{
                    if (ED2KHexHash != "" && TigerTreeRootBase32Hash != "" && SHA1Base32Hash != "" && LinkedFilename != "")
						return MagnetBitPrintPrefix + SHA1Base32Hash + "." + TigerTreeRootBase32Hash + MagnetED2KMidfix + ED2KHexHash+ "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}
			public string MagnetSHA1Hybrid
			{
				get
				{
					if (ED2KHexHash != "" && SHA1Base32Hash != "" && LinkedFilename != "")
						return MagnetSHA1Prefix + SHA1Base32Hash + MagnetED2KMidfix + ED2KHexHash + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}
			public string MagnetBitPrint
			{
				get
				{
					if (TigerTreeRootBase32Hash != "" && SHA1Base32Hash != "" && LinkedFilename != "")
						return MagnetBitPrintPrefix + SHA1Base32Hash + "." + TigerTreeRootBase32Hash + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}
			public string MagnetSHA1
			{
				get
				{
					if (SHA1Base32Hash != "" && LinkedFilename != "")
                        return MagnetSHA1Prefix + SHA1Base32Hash + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}
			public string MagnetED2K
			{
				get
				{
					if (ED2KHexHash != "" && LinkedFilename != "")
                        return MagnetED2KPrefix + ED2KHexHash + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}
			public string ClassicED2KLink
			{
				get
				{
					if (ED2KHexHash != "" && LinkedFilename != "" && LinkedFileSize > 0)
                        return ED2KPrefix + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "|" + LinkedFileSize + "|" + ED2KHexHash + "|/";
					else
						return "";
				}
			}
		}
	}
	
}