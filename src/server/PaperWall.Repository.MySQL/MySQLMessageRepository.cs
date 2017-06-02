using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using PaperWall.Core.DomainObject;
using PaperWall.Core.Repository;

namespace PaperWall.Repository.MySQL
{
    public class MySQLMessageRepository: IMessageRepository
    {
        private MySqlConnection _connection;

        public List<Message> GetByLocation(double latitude, double longitude, double precision)
        {
            var queryResult = new List<Message>();
            using (var transaction = _connection.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                var cmd = _connection.CreateCommand();
                cmd.CommandText = ProximityProvider.GetProximityAlgorithm(latitude,longitude,precision);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    queryResult.Add(new Message
                                        {
                                            Id = reader.GetInt64("Id"),
                                            Latitude = reader.GetDouble("Latitude"),
                                            Longitude = reader.GetDouble("Longitude"),
                                            Precision = reader.GetDouble("Precision"),
                                            MessageText = reader.GetString("MessageText"),
                                            Writter = reader.GetString("Writter"),
                                            PostedAt = reader.GetDateTime("PostedAt"),
                                            Removed = reader.GetBoolean("Removed")
                                        });
                }

                reader.Close();
                reader.Dispose();
                cmd.Dispose();

                transaction.Commit();
            }
            return queryResult;
        }

        public Message Get(long messageId)
        {
            Message queryResult = null;
            using (var transaction = _connection.BeginTransaction(IsolationLevel.ReadUncommitted))
            {                
                var cmd = _connection.CreateCommand();
                cmd.CommandText = @"Select * From Messages Where Id = @1";
                cmd.Parameters.AddWithValue("@1", messageId);
                var reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    queryResult = new Message
                                 {
                                     Id = reader.GetInt64("Id"),
                                     Latitude = reader.GetDouble("Latitude"),
                                     Longitude = reader.GetDouble("Longitude"),
                                     Precision = reader.GetDouble("Precision"),
                                     MessageText = reader.GetString("MessageText"),
                                     Writter = reader.GetString("Writter"),
                                     PostedAt = reader.GetDateTime("PostedAt"),
                                     Removed = reader.GetBoolean("Removed")
                                 };
                }

                reader.Close();
                reader.Dispose();
                cmd.Dispose();

                transaction.Commit();
            }
            return queryResult;
        }

        public bool Remove(long messageToRemoveId)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                var cmd = _connection.CreateCommand();
                cmd.CommandText = @"Update Messages Set Removed = 1 Where Id = @1";
                cmd.Parameters.AddWithValue("@1", messageToRemoveId);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                
                transaction.Commit();
            }
            return true;
        }

        public Message Remove(Message messageToRemove)
        {
            messageToRemove.Removed = Remove(messageToRemove.Id);
            return messageToRemove;
        }

        public bool Save(Message messageToSave)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                var cmd = _connection.CreateCommand();
                cmd.CommandText =
                    @"Insert Into Messages 
                                  (Latitude, Longitude, `Precision`, MessageText, Writter, PostedAt, Removed)
                                  values
                                  (@latitude, @longitude, @precision, @messageText, @writter, @postedAt, @Removed)";
                cmd.Parameters.AddWithValue("@latitude", messageToSave.Latitude);
                cmd.Parameters.AddWithValue("@longitude", messageToSave.Longitude);
                cmd.Parameters.AddWithValue("@precision", messageToSave.Precision);
                cmd.Parameters.AddWithValue("@messageText", messageToSave.MessageText);
                cmd.Parameters.AddWithValue("@writter", messageToSave.Writter);
                cmd.Parameters.AddWithValue("@postedAt", messageToSave.PostedAt);
                cmd.Parameters.AddWithValue("@Removed", messageToSave.Removed);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

                transaction.Commit();
            }
            return true;
        }

        public void Initialize()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            _connection = _connection ?? new MySqlConnection(connectionString);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        public void Finalize()
        {
            if (_connection.State != ConnectionState.Open)
                return;
            _connection.Close();
            _connection.Dispose();
        }
    }
}
