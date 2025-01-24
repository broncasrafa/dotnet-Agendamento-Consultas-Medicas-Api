using RSF.AgendamentoConsultas.Core.Domain.Notifications;
using Amazon.S3;
using Amazon.S3.Model;

namespace RSF.AgendamentoConsultas.Infra.Notifications.Templates;

public abstract class MailTemplateBase
{
    private readonly IMailSender _mailSender;
    private readonly IAmazonS3 _s3Client;

    protected MailTemplateBase(IMailSender mailSender, IAmazonS3 s3Client)
    {
        _mailSender = mailSender;
        _s3Client = s3Client;
    }

    protected async Task<string> GetHtmlTemplateFromS3Async(string key)
    {
        var request = new GetObjectRequest
        {
            BucketName = "rsfrancisco.agendamentoconsultas.emailtemplates",
            Key = key
        };

        using var response = await _s3Client.GetObjectAsync(request);
        using var reader = new StreamReader(response.ResponseStream);
        var template = await reader.ReadToEndAsync();

        var logo1 = await GetPreSignedUrlFromS3Async(bucketName: "rsfrancisco.agendamentoconsultas.images", key: "logo_1.png");
        var logo2 = await GetPreSignedUrlFromS3Async(bucketName: "rsfrancisco.agendamentoconsultas.images", key: "logo_3.png");

        template = template.Replace("{{Logo1}}", logo1).Replace("{{Logo2}}", logo2);
        return template;
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