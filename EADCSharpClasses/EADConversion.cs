/*
 * Enlist-a-distro Common C# classes
 * CoreHash Class
 * Constants Class
 * Hash display type Conversion
 * Byte Array - Raw - Hexadecimal
 * Code by Harold Feit - Depthstrike.com/Enlist-a-Distro
 * ToDo:
 * Add directional support for Base32 to EAD.Conversion.EADCoreHash
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
			}
		}
		
		namespace Conversion
		{
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