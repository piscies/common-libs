using Piscies.Common.Crosscut.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Piscies.Common.Crosscut.Helpers
{
    public class ActionResponseWrapper
    {
        #region Properties

        private ActionResponseDTO actionResponse;
        string context;

        #endregion

        public ActionResponseWrapper(string context)
        {
            actionResponse = new ActionResponseDTO();
            actionResponse.Errors = new List<ActionErrorDTO>();
            this.context = context;
        }

        #region Properties

        public ActionResponseDTO Value
        {
            get
            {
                ActionResponseDTO returnedValue = new ActionResponseDTO();
                returnedValue.Errors = new List<ActionErrorDTO>();
                returnedValue.Content = actionResponse.Content;

                foreach (ActionErrorDTO error in actionResponse.Errors)
                {
                    ActionErrorDTO returnedError = new ActionErrorDTO();
                    returnedError.Context = error.Context;
                    returnedError.Field = error.Field;
                    returnedError.Message = error.Message;

                    returnedValue.Errors.Add(returnedError);
                }

                return returnedValue;
            }
        }

        public bool HasErrors
        {
            get
            {
                return actionResponse.Errors.Count > 0;
            }
        }

        #endregion

        #region Public Methods

        public void AddError(string field, string message)
        {
            ActionErrorDTO newError = new ActionErrorDTO();
            newError.Context = context;
            newError.Field = field;
            newError.Message = message;

            actionResponse.Errors.Add(newError);
        }
        public void AddError(string message)
        {
            AddError("", message);
        }

        public void SetContent(object content)
        {
            actionResponse.Content = content;
        }

        public void ValidateMandatoryField(object value, string fieldName, string errorMessage)
        {
            bool isValid = true;

            //General mandatory field rules
            if (value == null)
                isValid = false;
            //Field type validation rules
            else
            {
                Type fieldType = value.GetType();

                //String mandatory field rules
                if (fieldType == typeof(string))
                {
                    if (string.IsNullOrWhiteSpace((string)value))
                        isValid = false;
                }

                //DateTime mandatory field rules
                else if (fieldType == typeof(DateTime))
                {
                    if ((DateTime)value == DateTime.MinValue)
                        isValid = false;
                }
            }

            //Throws the exception
            if (!isValid)
                AddError(fieldName, errorMessage);
        }

        public ActionErrorDTO ValidateIntegerGreaterThanZero(int value, string fieldName, string errorMessage)
        {
            ActionErrorDTO raisedError = null;

            if (value <= 0)
                AddError(fieldName, errorMessage);

            return raisedError;
        }

        public void IncorporateActionResponse(ActionResponseDTO newActionResponse)
        {
            foreach (ActionErrorDTO error in newActionResponse.Errors)
                actionResponse.Errors.Add(error);
        }
        #endregion
    }
}
