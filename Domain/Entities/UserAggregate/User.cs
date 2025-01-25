using Domain.Entities.Exceptions;
using Domain.Helpers;
using Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.UserAggregate
{
    public class User
    {
        private readonly string _password = string.Empty;
        private readonly string _email = string.Empty;

        private readonly string _name = string.Empty;

        [BsonId] // Marca esta propriedade como o identificador do documento
        [BsonRepresentation(BsonType.ObjectId)] // Informa que o campo `_id` é um ObjectId
        public string Id { get; set; }

        public required string Name
        {
            get => _name;
            init
            {
                EntityArgumentNullException.ThrowIfNullOrWhiteSpace(
                    value,
                    nameof(Name),
                    GetType()
                );

                _name = value;
            }
        }

        public required string Email
        {
            get => _email;
            init
            {
                EntityArgumentNullException.ThrowIfPropertyNull(
                    value,
                    nameof(Email),
                    GetType()
                );

                _email = value;
            }
        }


        public required string Password
        {
            get => _password;
            init
            {
                EntityArgumentNullException.ThrowIfPropertyNull(
                    value,
                    nameof(Password),
                    GetType()
                );

                _password = value; 
            }
        }

    }
}