using flamflam.SubmissionService.Core.Errors;
using MongoDB.Driver;

namespace flamflam.SubmissionService.Core.Database
{
    public class MongoDbExceptionHandler : IAppExceptionHandler
    {
        private static readonly ApiErrorResponse DefaultError =
            new ApiErrorResponse(
                StatusCodes.Status500InternalServerError,
                "Failed to execute operation on the database"
            );

        private static readonly ApiErrorResponse TimeoutError =
            new ApiErrorResponse(
                StatusCodes.Status500InternalServerError,
                "Failed to execute operation on the database due to timeout"
            );

        private static readonly ApiErrorResponse DuplicateKeyError =
            new ApiErrorResponse(
                StatusCodes.Status409Conflict,
                "Record with the matching key already exists"
            );

        public bool IsExceptionSupported(Exception ex)
        {
            return ex is MongoException;
        }

        public ApiErrorResponse ToApiErrorResponse(Exception ex)
        {
            var response = DefaultError;

            if (ex is MongoWriteException mwex)
            {
                var mwexResponse = MapWriteException(mwex);
                if (mwexResponse != null)
                {
                    response = mwexResponse;
                }
            }

            return response;
        }

        private ApiErrorResponse? MapWriteException(MongoWriteException ex)
        {
            var writeError = ex.WriteError;
            if (writeError == null) return null;

            var category = writeError.Category;
            switch (category)
            {
                case ServerErrorCategory.DuplicateKey:
                    return DuplicateKeyError;
                case ServerErrorCategory.ExecutionTimeout:
                    return TimeoutError;
                default:
                    return null;
            }
        }
    }
}
