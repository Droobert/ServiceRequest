using Amazon.S3;
using Amazon.S3.Transfer;
using System.IO;
using Amazon.Runtime;
//using Amazon;
//using com.amazonaws.services.s3.AmazonS3Client;
//using Com.Cloudrail.SI.Services;
//using Amazon.S3;

namespace DoYourJob
{
    class AWSHelper
    {
        public string AccessKey = "AKIAJNBPFMULG57DJUTQ";
        public string SecretKey = "R95sP0V8x6sVYp-MY3yWxH7CbOBKP+34RcJr98F";
        public string bucketName = "matinence-services";
        AmazonS3Client s3Client; 
        TransferUtility transferUtility;

        public void createClient()
        {
            BasicAWSCredentials credentials = new BasicAWSCredentials(AccessKey, SecretKey);
            //RegionEndpoint endpoint = new RegionEndpoint();
            //string region = RegionEndpoint.GetEndpointForService("us-east1");
            //string region = "string";
            s3Client = new AmazonS3Client(AccessKey, SecretKey);
            transferUtility = new TransferUtility(s3Client);
        }

        public void uploadFile(string file)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            transferUtility.UploadAsync(Path.Combine(path, file), bucketName);
        }
    }
}