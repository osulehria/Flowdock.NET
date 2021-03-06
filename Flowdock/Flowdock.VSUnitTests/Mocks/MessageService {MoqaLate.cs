using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flowdock.Services.Message;
namespace MoqaLate.Autogenerated
{
public partial class MessageService {MoqaLate : IMessageService {
{


// -------------- ShowError ------------


        private int _showErrorNumberOfTimesCalled;        

        public Exception ShowErrorParameter_e_LastCalledWith;

        public virtual bool ShowErrorWasCalled()
{
   return _showErrorNumberOfTimesCalled > 0;
}


public virtual bool ShowErrorWasCalled(int times)
{
   return _showErrorNumberOfTimesCalled == times;
}


public virtual int ShowErrorTimesCalled()
{
   return _showErrorNumberOfTimesCalled;
}


public virtual bool ShowErrorWasCalledWith(Exception e){
return (
e.Equals(ShowErrorParameter_e_LastCalledWith) );
}


        public void ShowError(Exception e)
        {
            _showErrorNumberOfTimesCalled++;            

            ShowErrorParameter_e_LastCalledWith = e;
        }
}
}
