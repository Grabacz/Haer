using DG.Haer.Data;
using System;
using System.Configuration;

namespace DG.Haer.Api
{
    public class DbProvider : IDbProvider
    {
        private const string DbName = "HaerDb";

        public string ConnectionString
        {
            get
            {
                var connectionStringSetting = ConfigurationManager.ConnectionStrings[DbName];

                if (connectionStringSetting == null)
                    throw new InvalidOperationException($"Database with specific name {DbName} does not exist!");

                return connectionStringSetting.ConnectionString;
            }
        }
    }
}
