/*
 * Enlist-a-distro Common C# classes
 * LinkGeneration class
 * Converts ED2K, SHA1 and Tiger Tree hashes to usable p2p network links.
 * Code by Harold Feit - Depthstrike.com/Enlist-a-Distro
 * ToDo:
 * Add support for static sources in URLs
 */

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
			private string ED2KRawHash;
			private string SHA1RawHash;
			private string SHA1Base32Hash;
			private string TigerTreeRootRawHash;
			private string TigerTreeBase32Hash;
			private string LinkedFilename;
			private long LinkedFileSize;
			private const string MagnetPrefix = "magnet:?";
			private const string MagnetSHA1Prefix = "xt=urn:sha1:";
			private const string MagnetBitPrintPrefix = "xt=urn:bitprint:";
			private const string MagnetED2KPrefix = "xt=urn:ed2khash:";
			private const string ED2KPrefix = "ed2k://|file|";
			private EAD.Conversion.HashChanger convertme = new EAD.Conversion.HashChanger();
			// Hexadecimal Hash Value Inputs
			// SHA1
			public string SHA1Hex
			{
				set
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.hexhash = value;
					SHA1RawHash = convertme.rawhash;
					SHA1Base32Hash = convertme.base32;
				}
				get
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = SHA1RawHash;
					return convertme.hexhash;
				}
			}
			// Tiger Tree
			public string TigerHex
			{
				set
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.hexhash = value;
					TigerTreeRootRawHash = convertme.rawhash;
					TigerTreeBase32Hash = convertme.base32;
				}
				get
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = TigerTreeRootRawHash;
					return convertme.hexhash;
				}
			}

			// ED2K
			public string ED2KHex
			{
				set
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.hexhash = value;
					ED2KRawHash = convertme.rawhash;
				}
				get
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = ED2KRawHash;
					return convertme.hexhash;
				}
			}
			// Raw Hash value Inputs
			// SHA1
			public string SHA1Raw
			{
				set
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = value;
					SHA1Base32Hash = convertme.base32;
					SHA1RawHash = value;
				}
				get
				{
					return SHA1RawHash;
				}
			}
			// Tiger Tree
			public string TigerRaw
			{
				set
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = value;
					TigerTreeBase32Hash = convertme.base32;
					TigerTreeRootRawHash = value;
				}
				get
				{
					return TigerTreeRootRawHash;
				}
			}
			// ED2K
			public string ED2KRaw
			{
				set
				{
					ED2KRawHash = value;
				}
				get
				{
					return ED2KRawHash;
				}
			}

			// Byte Array Values
			// SHA1
			public byte[] SHA1Bytes
			{
				set
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.bytehash = value;
					SHA1RawHash = convertme.rawhash;
				}
				get
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = SHA1RawHash;
					return convertme.bytehash;
				}
			}
			// Tiger Tree
			public byte[] TigerBytes
			{
				set
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.bytehash = value;
					TigerTreeRootRawHash = convertme.rawhash;
				}
				get
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = TigerTreeRootRawHash;
					return convertme.bytehash;
				}
			}
			// ED2K
			public byte[] ed2kbytes
			{
				set
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.bytehash = value;
					ED2KRawHash = convertme.rawhash;
				}
				get
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = ED2KRawHash;
					return convertme.bytehash;
				}
			}
			// Base 32 Output
			// SHA1
			public string SHA1Hash
			{
				get
				{
					return SHA1Base32Hash;
				}
				set
				{
					SHA1Base32Hash = value;
					SHA1RawHash = null;
				}
			}
			// Tiger Tree
			public string TTH
			{
				get
				{
					return TigerTreeBase32Hash;
				}
				set
				{
					TigerTreeBase32Hash = value;
					TigerTreeRootRawHash = null;
				}
			}

			// Values
			// Filename
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
			// File Size
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
					EAD.Conversion.HashChanger TigerConvert = new EAD.Conversion.HashChanger();
					TigerConvert.rawhash = TigerTreeRootRawHash;
					EAD.Conversion.HashChanger SHA1Convert = new EAD.Conversion.HashChanger();
					SHA1Convert.rawhash = SHA1RawHash;
					EAD.Conversion.HashChanger ED2KConvert = new EAD.Conversion.HashChanger();
					ED2KConvert.rawhash = ED2KRawHash;
                    if (ED2KRawHash != "" && TigerConvert.base32 != "" && SHA1Convert.base32 != "" && LinkedFilename != "")
						return MagnetPrefix + MagnetBitPrintPrefix + SHA1Convert.base32 + "." + TigerConvert.base32 + "&" + MagnetED2KPrefix + ED2KConvert.hexhash + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}
			public string MagnetSHA1Hybrid
			{
				get
				{
					EAD.Conversion.HashChanger ED2KConvert = new EAD.Conversion.HashChanger();
					ED2KConvert.rawhash = ED2KRawHash;
					EAD.Conversion.HashChanger SHA1Convert = new EAD.Conversion.HashChanger();
					SHA1Convert.rawhash = SHA1RawHash;
					if (ED2KConvert.hexhash != "" && SHA1Convert.base32 != "" && LinkedFilename != "")
						return MagnetPrefix + MagnetSHA1Prefix + SHA1Convert.base32 + "&" +  MagnetED2KPrefix + ED2KConvert.hexhash + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}
			public string MagnetBitPrint
			{
				get
				{
					EAD.Conversion.HashChanger TigerConvert = new EAD.Conversion.HashChanger();
					TigerConvert.rawhash = TigerTreeRootRawHash;
					EAD.Conversion.HashChanger SHA1Convert = new EAD.Conversion.HashChanger();
					SHA1Convert.rawhash = SHA1RawHash;
					if (TigerConvert.base32 != "" && SHA1Convert.base32 != "" && LinkedFilename != "")
						return MagnetPrefix + MagnetBitPrintPrefix + SHA1Convert.base32 + "." + TigerConvert.base32 + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}
			public string MagnetSHA1
			{
				get
				{
					EAD.Conversion.HashChanger SHA1Convert = new EAD.Conversion.HashChanger();
					SHA1Convert.rawhash = SHA1RawHash;
					if (SHA1Convert.base32 != "" && LinkedFilename != "")
                        return MagnetPrefix + MagnetSHA1Prefix + SHA1Convert.base32 + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}
			public string MagnetED2K
			{
				get
				{
					EAD.Conversion.HashChanger ED2KConvert = new EAD.Conversion.HashChanger();
					ED2KConvert.rawhash = ED2KRawHash;
					if (ED2KConvert.hexhash != "" && LinkedFilename != "")
                        return MagnetPrefix + MagnetED2KPrefix + ED2KConvert.hexhash + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}
			public string ClassicED2KLink
			{
				get
				{
					EAD.Conversion.HashChanger ED2KConvert = new EAD.Conversion.HashChanger();
					ED2KConvert.rawhash = ED2KRawHash;
					if (ED2KConvert.hexhash != "" && LinkedFilename != "" && LinkedFileSize > 0)
                        return ED2KPrefix + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "|" + LinkedFileSize + "|" + ED2KConvert.hexhash + "|/";
					else
						return "";
				}
			}
		}
	}
	
}