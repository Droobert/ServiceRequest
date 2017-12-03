using System;
using System.IO;
using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
//using Amazon;
//using com.amazonaws.services.s3.AmazonS3Client;
//using Com.Cloudrail.SI.Services;
//using Amazon.S3;

namespace DoYourJob
{
    public class UploadObjectUsingPresignedURL
    {
        static IAmazonS3 s3Client;
        // File to upload.
        static string FilePath;
        // Information to generate pre-signed object URL.
        //static string ObjectKey;
        static string BucketName = "matinence-services";
        const string AccessKey = "AKIAJNBPFMULG57DJTUTQ";
        const string SecretKey = "R95sP050V8x6sVYp-MY3yWxH7CbobKP+34RcJr98F";
        //s3Client 

        public UploadObjectUsingPresignedURL(string filePath) // string objectKey)
        {
            FilePath = filePath;
            s3Client = new AmazonS3Client(AccessKey, SecretKey, Amazon.RegionEndpoint.USEast1);                                        
            string url = GeneratePreSignedURL();
            UploadObject(url);
        }
    

        public void UploadObject(string url)
        {
            HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
            httpRequest.Method = "PUT";
            using (Stream dataStream = httpRequest.GetRequestStream())
            {
                byte[] buffer = new byte[8000];
                using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        dataStream.Write(buffer, 0, bytesRead);
                    }
                }
            }

            HttpWebResponse response = httpRequest.GetResponse() as HttpWebResponse;
        }

        static string GeneratePreSignedURL()
        {
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
            {
                BucketName = BucketName,
                //Key = ObjectKey,
                Verb = HttpVerb.PUT,
                Expires = DateTime.Now.AddMinutes(5)
            };

            string url = null;
            url = s3Client.GetPreSignedURL(request);
            return url;
        }
    }
}