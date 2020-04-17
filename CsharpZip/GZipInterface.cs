using System;

using System.IO;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.GZip;


namespace CSharpZip
{
	/// <summary>
	/// Summary description for GZipInterface.
	/// </summary>
	public class GZipInterface
	{
		public   static   int  GZipComp(byte []srcBuf, int srcIndex,  int srcLen,byte[]   DstBuf,int dstIndex,int dstLen, bool bCompress) 
		{ 
			int retLen = -1;
			try 
			{ 
				if(bCompress)
				{
					MemoryStream   msIn   =   new  MemoryStream(srcBuf,srcIndex,srcLen,false); 
					MemoryStream   msOut  =   new  MemoryStream(DstBuf,dstIndex,dstLen,true); 
			

					//   Use   the   newly   created   memory   stream   for   the   compressed   data. 
					using (Stream fsOut = new GZipOutputStream(msOut))
					{
						byte[] writeData = new byte[4096];
						StreamUtils.Copy(msIn, fsOut, writeData);
					}
					retLen = (int)msOut.Position;
					msIn.Close();
					msOut.Close();
				}
				else
				{
					MemoryStream   msIn   =   new  MemoryStream(srcBuf,srcIndex,srcLen,false); 
					MemoryStream   msOut  =   new  MemoryStream(DstBuf,dstIndex,dstLen,true); 

					using (Stream inStream = new GZipInputStream(msIn))
					{
						byte[] writeData = new byte[4096];
						StreamUtils.Copy(inStream, msOut, writeData);
					}
					retLen = (int) msOut.Position;
					msIn.Close();
					msOut.Close();
				}
				return retLen;
			}   //   end   try 
			catch( Exception ex) 
			{ 
				Console.WriteLine( ex.Message); 
				return retLen;
			} 
		}

	    /// <summary>
	    /// Extract the contents of a zip file.
	    /// </summary>
	    /// <param name="zipFileName">The zip file to extract from.</param>
	    /// <param name="targetDirectory">The directory to save extracted information in.</param>
	    /// <param name="fileFilter">A filter to apply to files.</param>
	    public static void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
	    {
	        FastZip fastZip = new FastZip();
            fastZip.ExtractZip(zipFileName,targetDirectory,fileFilter);
	    }

	}
}
