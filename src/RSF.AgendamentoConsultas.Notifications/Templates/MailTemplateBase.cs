﻿using RSF.AgendamentoConsultas.Domain.Notifications;
using Amazon.S3;
using Amazon.S3.Model;

namespace RSF.AgendamentoConsultas.Notifications.Templates;

public abstract class MailTemplateBase
{
    private readonly IMailSender _mailSender;
    private readonly IAmazonS3 _s3Client;

    protected MailTemplateBase(IMailSender mailSender, IAmazonS3 s3Client)
    {
        _mailSender = mailSender;
        _s3Client = s3Client;
    }

    protected async Task<string> GetHtmlTemplateFromS3Async(string bucketName, string key)
    {
        var request = new GetObjectRequest
        {
            BucketName = bucketName,
            Key = key
        };

        using var response = await _s3Client.GetObjectAsync(request);
        using var reader = new StreamReader(response.ResponseStream);
        return await reader.ReadToEndAsync();
    }

    protected async Task<string> GetPreSignedUrlFromS3Async(string bucketName, string key)
    {
        var request = new GetObjectRequest { BucketName = bucketName, Key = key };
        using var response = await _s3Client.GetObjectAsync(request);
        var urlRequest = new GetPreSignedUrlRequest()
        {
            BucketName = bucketName,
            Key = response.Key,
            Expires = DateTime.UtcNow.AddYears(1)
        };
        return _s3Client.GetPreSignedURL(urlRequest);
    }

    protected async Task SendEmailAsync(MailTo to, string subject, string body)
        => await _mailSender.SendMailAsync(to, subject, body);
}