using Example.MongoDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Example
{
    public class MongoDBExample : IExample
    {
        public async Task StartAsync()
        {
            var repository = new UserMongoEntityRepository();
            await OptionOneAsync(repository);
            await OptionManyAsync(repository);
        }

        private async Task OptionOneAsync(UserMongoEntityRepository repository)
        {
            var user1 = new UserMongoEntity
            {
                ID = Guid.NewGuid(),
                Name = "Materal",
                Age = 26
            };
            var user2 = new UserMongoEntity
            {
                ID = Guid.NewGuid(),
                Name = "Materal2",
                Age = 27
            };
            try
            {
                await repository.InsertAsync(user1);
                user1.Name = "Materal1";
                await repository.SaveAsync(user1);
                await repository.SaveAsync(user2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                await repository.DeleteAsync(user1);
                await repository.DeleteAsync(user2);
            }
        }

        private async Task OptionManyAsync(UserMongoEntityRepository repository)
        {
            var users = new List<UserMongoEntity>
            {
                new UserMongoEntity
                {
                    ID = Guid.NewGuid(),
                    Name = "Materal",
                    Age = 26
                },
                new UserMongoEntity
                {
                    ID = Guid.NewGuid(),
                    Name = "Materal2",
                    Age = 27
                },
                new UserMongoEntity
                {
                    ID = Guid.NewGuid(),
                    Name = "Materal3",
                    Age = 28
                }
            };
            try
            {
                await repository.InsertManyAsync(users);
                foreach (UserMongoEntity user in users)
                {
                    user.Name += "A";
                }
                await repository.SaveManyAsync(users);
                await repository.DeleteManyAsync(users);
                await repository.SaveManyAsync(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                await repository.DeleteManyAsync(users);
            }
        }
    }
}
