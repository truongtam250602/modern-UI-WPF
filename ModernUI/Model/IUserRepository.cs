using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModernUI.Model
{
    internal interface IUserReposity
    {
        bool AuthenticateUser(NetworkCredential credential); // kiem tra nguoi dung thuc bang lop NetworkCredential
        void Add(UserModel userModel); //Vi da su dung lop NetworkCredential nen dung lop UserModel 
        void Edit(UserModel userModel);
        void Remove(int id);
        UserModel GetById(int id);
        UserModel GetByUserName(string userName);
        IEnumerable<UserModel> GetByAll();


    }
}
