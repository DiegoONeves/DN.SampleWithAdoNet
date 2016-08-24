using DN.SampleWithAdoNet.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DN.SampleWithAdoNet.DomainModel
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(AdoNetContext context)
            : base(context)
        {
        }

        public void Create(User user)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = @"INSERT INTO [dbo].[USER] ([ID], [NAME], [EMAIL], [PASSWORD]) VALUES(@ID, @NAME, @EMAIL, @PASSWORD)";
                command.AddParameter("ID", user.Id);
                command.AddParameter("NAME", user.Name);
                command.AddParameter("EMAIL", user.Email);
                command.AddParameter("PASSWORD", user.Password);
                command.ExecuteNonQuery();
            }
        }
        public void Update(User user)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = @"UPDATE [dbo].[USER] SET NAME = @NAME, EMAIL = @EMAIL WHERE ID = @ID";
                command.AddParameter("ID", user.Id);
                command.AddParameter("NAME", user.Name);
                command.AddParameter("EMAIL", user.Email);
                command.ExecuteNonQuery();
            }
        }
        public void Delete(Guid id)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = @"DELETE FROM [dbo].[USER] WHERE ID = @ID";
                command.AddParameter("ID", id);
                command.ExecuteNonQuery();
            }
        }

        public User GetById(Guid id)
        {
            using (var command = _context.CreateCommand())
            {
                var user = new User();
                command.CommandText = @"SELECT ID,NAME,EMAIL FROM [dbo].[USER] (NOLOCK) WHERE ID = @ID";
                command.AddParameter("ID", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Map(reader, user);
                    }
                    return user;
                }
            }
        }

        public IEnumerable<User> ListAll()
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandText = @"SELECT * FROM [dbo].[USER]";
                return ToList(command);
            }
        }

       
        protected override void Map(IDataRecord record, User user)
        {
            user.Id = Guid.Parse(record["ID"].ToString());
            user.Name = (string)record["NAME"];
            user.Email = (string)record["EMAIL"];
        }
    }
}
