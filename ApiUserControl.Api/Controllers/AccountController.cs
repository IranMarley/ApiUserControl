using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ApiUserControl.Api.Models;
using ApiUserControl.Common.Resources;
using ApiUserControl.Domain.Contracts.Services;

namespace ApiUserControl.Api.Controllers
{
    [RoutePrefix("api/users")]
    public class AccountController : ApiController
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        [Route("")]
        public Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var users = _userService.GetByRange(0, 10);
                response = Request.CreateResponse(HttpStatusCode.OK, new { users });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        [Route("")]
        public Task<HttpResponseMessage> Post(RegisterUserModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _userService.Register(model.Name, model.Email, model.Password, model.ConfirmPassword);
                response = Request.CreateResponse(HttpStatusCode.OK, new { name = model.Name, email = model.Email });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [Authorize]
        [HttpPut]
        [Route("")]
        public Task<HttpResponseMessage> Put(ChangeInformationModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _userService.ChangeInformation(User.Identity.Name, model.Name);
                response = Request.CreateResponse(HttpStatusCode.OK, new { name = model.Name });
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [Authorize]
        [HttpPost]
        [Route("changepassword")]
        public Task<HttpResponseMessage> ChangePassword(ChangePasswordModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _userService.ChangePassword(User.Identity.Name, model.Password, model.NewPassword, model.NewConfirmPassword);
                response = Request.CreateResponse(HttpStatusCode.OK, Messages.PasswordSuccessfulyChanges);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [HttpPost]
        [Route("resetpassword")]
        public Task<HttpResponseMessage> ResetPassword(ResetPasswordModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                var password = _userService.ResetPassword(model.Email);
                response = Request.CreateResponse(HttpStatusCode.OK, string.Format(Messages.ResetPasswordEmailBody, password));
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        [Authorize]
        [HttpPost]
        [Route("removeuser")]
        public Task<HttpResponseMessage> RemoveUser(DeleteUserModel model)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                _userService.Delete(User.Identity.Name, model.Email);
                response = Request.CreateResponse(HttpStatusCode.OK, Messages.RemoveUser);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        protected override void Dispose(bool disposing)
        {
            _userService.Dispose();
        }
    }
}