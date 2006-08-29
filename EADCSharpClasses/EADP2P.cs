/*
 * Enlist-a-distro Common C# classes
 * LinkGeneration class
 * Converts ED2K, SHA1 and Tiger Tree hashes to usable p2p network links.
 * Code by Harold Feit - Depthstrike.com/Enlist-a-Distro
 * ToDo:
 * Add support for static sources in URLs
'Copyright (c) 2006, Depthstrike Entertainment.
'Module Author - Harold Feit - dwknight@depthstrike.com
'All rights reserved.
'
'Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
'
'* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
'* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
'* Neither the name of the Depthstrike Entertainment nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
'* Use of this package or module without this license agreement present is only permitted with the express permission of Depthstrike Entertainment administration or the author of the module, either in writing or electronically with the digital PGP/GPG signature of a Depthstrike Entertainment administrator or the author of the module.
'
' THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
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
			private string BTInfoRaw;
			private string BTInfoBase32;
			private string LinkedFilename;
			private long LinkedFileSize;
			private const string MagnetPrefix = "magnet:?";
			private const string MagnetSHA1Prefix = "xt=urn:sha1:";
			private const string MagnetTigerPrefix = "xt=urn:tree:tiger:";
			private const string MagnetBitPrintPrefix = "xt=urn:bitprint:";
			private const string MagnetED2KPrefix = "xt=urn:ed2k:";
			private const string MagnetBTIHPrefix = "xt=urn:btih:";
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
			// BTIH
			public string BTIHHex
			{
				set
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.hexhash = value;
					BTInfoRaw = convertme.rawhash;
					BTInfoBase32 = convertme.base32;
				}
				get
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = BTInfoRaw;
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
			public string BTInfoHashRaw
			{
				set
				{
					BTInfoRaw = value;
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = value;
					BTInfoBase32 = convertme.base32;
				}
				get
				{
					return BTInfoRaw;
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
			// BTInfo
			public byte[] BTInfoBytes
			{
				set
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.bytehash = value;
					BTInfoRaw = convertme.rawhash;
					BTInfoBase32 = convertme.base32;
				}
				get
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = BTInfoRaw;
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
			// BTIH
			public string BTIH
			{
				get
				{
					return BTInfoBase32;
				}
				set
				{
					BTInfoBase32 = value;
					BTInfoRaw = null;
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
			public string MagnetBitPrintContent
			{
				get
				{
					if (TigerTreeBase32Hash != "" && SHA1Base32Hash != "")
						return MagnetBitPrintPrefix + SHA1Base32Hash + "." + TigerTreeBase32Hash;
					else
						return "";
				}
			}

			public string MagnetSHA1Content
			{
				get
				{
					if (SHA1Base32Hash != "")
						return MagnetSHA1Prefix + SHA1Base32Hash;
					else
						return "";
				}
			}

			public string MagnetED2KContent
			{
				get
				{
					convertme = new EAD.Conversion.HashChanger();
					convertme.rawhash = ED2KRawHash;
					if (convertme.rawhash != "")
						return MagnetED2KPrefix + convertme.hexhash;
					else
						return "";
				}
			}

			public string MagnetTigerTreeContent
			{
				get
				{
					if (TigerTreeBase32Hash != "")
						return MagnetTigerPrefix + TigerTreeBase32Hash;
					else
						return "";
				}
			}

			public string MagnetBTIHContent
			{
				get
				{
					if (BTInfoBase32 != "")
						return MagnetBTIHPrefix + BTInfoBase32;
					else
						return "";
				}
			}

			public string MagnetFull
			{
				get
				{
					if (MagnetBTIHContent !="" && MagnetBitPrintContent != "" && MagnetED2KContent != "" && LinkedFilename != "")
					{
						if (LinkedFileSize > 0)
							return MagnetPrefix + MagnetBitPrintContent + "&" + MagnetED2KContent + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "&xl=" + LinkedFileSize.ToString();
						else
							return MagnetPrefix + MagnetBitPrintContent + "&" + MagnetED2KContent + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					}
					else
						return "";
				}
			}


			public string MagnetTigerBTIH
			{
				get
				{
					if (MagnetTigerTreeContent != "" && MagnetBTIHContent != "" && LinkedFilename != "")
					{
						if (LinkedFileSize > 0)
							return MagnetPrefix + MagnetTigerTreeContent + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename)+ "&xl=" + LinkedFileSize.ToString();
						else
							return MagnetPrefix + MagnetTigerTreeContent + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					}
					else
						return "";
				}
			}

			public string MagnetTigerED2K
			{
				get
				{
					if (MagnetTigerTreeContent != "" && MagnetED2KContent != "" && LinkedFilename != "")
					{
						if (LinkedFileSize > 0)
							return MagnetPrefix + MagnetTigerTreeContent + "&" + MagnetED2KContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "&xl=" + LinkedFileSize.ToString();
						else
							return MagnetPrefix + MagnetTigerTreeContent + "&" + MagnetED2KContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					}
					else
						return "";
				}
			}

			public string MagnetTigerED2KBTIH
			{
				get
				{
					if (MagnetTigerTreeContent != "" && MagnetED2KContent != "" && MagnetBTIHContent != "" && LinkedFilename != "")
					{
						if (LinkedFileSize > 0)
							return MagnetPrefix + MagnetTigerTreeContent + "&" + MagnetED2KContent + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "&xl=" + LinkedFileSize.ToString();
						else
							return MagnetPrefix + MagnetTigerTreeContent + "&" + MagnetED2KContent + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					}
					else
						return "";
				}
			}


			public string MagnetBitPrintHybrid
			{
				get
				{
					if (MagnetBitPrintContent !="" && MagnetED2KContent != "" && LinkedFilename != "")
					{
						if (LinkedFileSize > 0 )
							return MagnetPrefix + MagnetBitPrintContent + "&" + MagnetED2KContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "&xl=" + LinkedFileSize.ToString();
						else
							return MagnetPrefix + MagnetBitPrintContent + "&" + MagnetED2KContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					}
					else
						return "";
				}
			}

			public string MagnetSHA1Hybrid
			{
				get
				{
					if (MagnetED2KContent != "" && MagnetSHA1Content != "" && LinkedFilename != "")
					{
						if (LinkedFileSize > 0)
							return MagnetPrefix + MagnetSHA1Content + "&" +  MagnetED2KContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "&xl=" + LinkedFileSize.ToString();
						else
							return MagnetPrefix + MagnetSHA1Content + "&" +  MagnetED2KContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					}
					else
						return "";
				}
			}

			public string MagnetBitPrintBTIH
			{
				get
				{
					if (MagnetBitPrintContent != "" && MagnetBTIHContent != "" && LinkedFilename != "")
					{
						if (LinkedFileSize > 0)
							return MagnetPrefix + MagnetBitPrintContent + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "&xl=" + LinkedFileSize.ToString();
						else
							return MagnetPrefix + MagnetBitPrintContent + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					}
					else
						return "";
				}
			}

			public string MagnetBitPrint
			{
				get
				{
					if (MagnetBitPrintContent != "" && LinkedFilename != "")
					{
						if (LinkedFileSize > 0)
							return MagnetPrefix + MagnetBitPrintContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "&xl=" + LinkedFileSize.ToString();
						else
							return MagnetPrefix + MagnetBitPrintContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					}
					else
						return "";
				}
			}

			public string MagnetSHA1BTIH
			{
				get
				{
					if (MagnetSHA1Content != "" && MagnetBTIHContent != "" && LinkedFilename != "")
					{
						if (LinkedFileSize > 0)
							return MagnetPrefix + MagnetSHA1Content + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "&xl=" + LinkedFileSize.ToString();
						else
							return MagnetPrefix + MagnetSHA1Content + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					}
					else
						return "";
				}
			}

			public string MagnetSHA1
			{
				get
				{
					if (MagnetSHA1Content != "" && LinkedFilename != "")
					{
						if (LinkedFileSize > 0)
							return MagnetPrefix + MagnetSHA1Content + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "&xl=" + LinkedFileSize.ToString();
						else
							return MagnetPrefix + MagnetSHA1Content + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					}
					else
						return "";
				}
			}

			public string MagnetBTIH
			{
				get
				{
					if (MagnetBTIHContent != "" && LinkedFilename != "")
						return MagnetPrefix + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}

			public string MagnetED2K
			{
				get
				{
					if (MagnetED2KContent != "" && LinkedFilename != "")
					{
						if (LinkedFileSize > 0)
							return MagnetPrefix + MagnetED2KContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename) + "&xl=" + LinkedFileSize.ToString();
						else
							return MagnetPrefix + MagnetED2KContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					}
					else
						return "";
				}
			}

			public string MagnetED2KBTIH
			{
				get
				{
					if (MagnetED2KContent != "" && MagnetBTIHContent != "" && LinkedFilename != "")
						return MagnetPrefix + MagnetED2KContent + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
					else
						return "";
				}
			}

			public string MagnetSHA1ED2KBTIH
			{
				get
				{
					if (MagnetSHA1Content != "" && MagnetED2KContent != "" && MagnetBTIHContent != "" && LinkedFilename != "")
						return MagnetPrefix + MagnetSHA1Content + "&" + MagnetED2KContent + "&" + MagnetBTIHContent + "&dn=" + System.Web.HttpUtility.UrlEncode(LinkedFilename);
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