namespace Example.App.Utility
{
    public static  class MiddleWareComponent
    {
        public static WebApplication UseDevelopmentConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }

        public static WebApplication UseStandardMiddleware(this WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseAuthorization();
            return app;
        }

        public static WebApplication UseEndpointsConfiguration(this WebApplication app)
        {
            app.MapControllers();
            return app;
        }
    }
}
