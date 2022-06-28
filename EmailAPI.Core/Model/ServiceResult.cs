using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EmailAPI.Core.Model
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            Errors = new Dictionary<string, string>();
            Succeeded = true;
        }

        public Dictionary<string, string> Errors { get; private set; }
        public bool Succeeded { get; private set; }
        
        public void AddError(string code, string message)
        {
            Errors.Add(code, message);

            Succeeded = false;
        }

        public ServiceResult MergeIdentityResult(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    AddError(error.Code, error.Description);
                }
            }

            return this;
        }

        [JsonIgnore]
        public static ServiceResult Success
        {
            get
            {
                return new ServiceResult { Succeeded = true };
            }
        }

        public static ServiceResult Failed(string code, string message)
        {
            var result = new ServiceResult();
            result.AddError(code, message);

            return result;
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }

        public ServiceResult() : base()
        {
        }
    }
}
