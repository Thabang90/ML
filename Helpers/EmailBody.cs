namespace QuestionExplorer.Helpers
{
    public static class EmailBody
    {
        public static string BuildForgotPasswordEmailBody(string email)
        {
            return $@"
                <html>
                    <head>
                    </head>
                    <body style=""margin: 0; padding: 0; font - family: Arial, Helvetica, sans - serif; "">
                        <div style=""height: auto; background: Linear - gradient(to top, #c9c9ff 50%, #6e6ef6 90%) no-repeat; width:400px; padding:30px"">
                            <div>
                                 <h1>Your Password</h1>
                                 <hr>
                                 <p>You're receiving this e-mail because you requested a password reset for your Question Explorer account.</p> 
                                 
                                 <p>Link: <a href=""http://localhost:4200/reset-password?email={email}"">Reset Password!</a></p>
                                 <p>Kind Regards, <br><br>
                                 Question Explorer Team</p>
                            </div
                        </div>
                    </body>
                </html>
            ";
        }

        public static string BuildNewUserPasswordEmailBody(string email,string password)
        {
            return $@"
                <html>
                    <head>
                    </head>
                    <body style=""margin: 0; padding: 0; font - family: Arial, Helvetica, sans - serif; "">
                        <div style=""height: auto; background: Linear - gradient(to top, #c9c9ff 50%, #6e6ef6 90%) no-repeat; width:400px; padding:30px"">
                            <div>
                                 <h1>Your Login Credentials</h1>
                                 <hr>
                                 <p>You're receiving this e-mail because a new user account has been created for you.</p> 

                                 <p>Username: {email}</p>   
                                 <p>Password: {password}</p> 

                                 <p>Link: <a href=""http://localhost:4200/login"">Login Now!</a></p>
                                 <p>Kind Regards, <br><br>
                                 Question Explorer Team</p>
                            </div
                        </div>
                    </body>
                </html>
            ";
        }
    }
}
