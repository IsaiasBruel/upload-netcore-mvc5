using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace upload_netcore_mvc2.Models
{
    public class AWSS3
    {
        private const string bucketName = "c3a-bucket";
        private const string awsID = "AKIAXF75UIOQYNEDCQOC";
        private const string awsKey = "Pb1RdZdca5dcIX4GGEtgzy6ga26lYog5IDEvU44l";

        public static string UploadS3(IFormFile arquivo)
        {
            var keyName = DateTime.Now.ToString("dd-MM-yyyy-HH-MM-") + Path.GetFileName(arquivo);

            //Amazon.Util.ProfileManager.RegisterProfile("gabriel", awsID, awsKey);
            var credentials = new BasicAWSCredentials(awsID, awsKey);
            var s3Client = new AmazonS3Client(credentials, RegionEndpoint.SAEast1);
            var fileTransferUtility = new TransferUtility(s3Client);

            fileTransferUtility.Upload(arquivo, bucketName, keyName);

            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest(); // request para trazer qual a URL que foi gerada
            request.BucketName = bucketName;
            request.Key = keyName;
            request.Expires = DateTime.Now.AddDays(1); // expira em 1 dia
            request.Protocol = Protocol.HTTP;

            return s3Client.GetPreSignedURL(request);

        }
    }
}
