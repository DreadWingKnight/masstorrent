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

			public class PortToByte
			{
				public int iPort;
				private byte[] pTempCompact;
				public byte[] pCompact
				{
					get
					{
						pTempCompact[0] = (byte)( ( iPort & 0xFF00 ) >> 8 );
						pTempCompact[1] = (byte)( iPort & 0xFF );
						return pTempCompact;
					}
				}
			}

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
						/*for ( int index = 0; index >= bytehashvalue.Length ; index++ )
						{
							rawhashvalue = rawhashvalue + System.Text.Encoding.Default.GetString(bytehashvalue,index,1);
						}
						*/
						int byteindex;
						byteindex = 0;
						foreach (byte HashByte in bytehashvalue)
						{
							rawhashvalue = rawhashvalue + System.Text.Encoding.Default.GetString(bytehashvalue,byteindex,1);
							buff.AppendFormat("{0:x2}", HashByte);
							byteindex++;
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
						bytehashvalue = System.Text.Encoding.Default.GetBytes(value);
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