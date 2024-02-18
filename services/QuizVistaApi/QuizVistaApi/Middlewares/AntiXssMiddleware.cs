using Ganss.Xss;
using System.Text;

namespace QuizVistaApi.Middlewares
{
    public class AntiXssMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Request.EnableBuffering();

            using (var streamReader = new StreamReader(context.Request.Body, encoding: Encoding.UTF8, leaveOpen: true))
            {
                var rawBody = await streamReader.ReadToEndAsync();

                var sanitazer = new HtmlSanitizer();
                var sanitizedBody = sanitazer.Sanitize(rawBody);

                if (rawBody != sanitizedBody)
                    throw new BadHttpRequestException("XSS ATTACK DETECTED");

            }

            context.Request.Body.Seek(0, SeekOrigin.Begin);

            await next.Invoke(context);


        }
    }
}
