using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using PublicTheater.Custom.Mail2REST;

namespace PublicTheater.Custom.Mail2REST
{
    public class mail2
    {
        private Transport transport;

        public mail2(string api_key, bool use_https, string apiUrl = "")
        {
            transport = new Transport(api_key, use_https, "",apiUrl);
        }



        /**
         * <summary>Upload csv data for mapping and importing via the mail2 UI</summary>
         *
         * 
         *
         * <param>stringcsv Your CSV data</param>
         * <returns>object A struct containing a path key to be used with the webapp</returns>
         */
        public object Integration_Upload_Csv(string csv)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("csv", csv);

            return transport.MakeCall("Integration_Upload_Csv", args);
        }


        /**
         * <summary>Integrate mail2 into your own web application by setting your CAMPAIGNS_SSO_COOKIE</summary>
         *
         * Please contact mail2 support for details, this function is only available to reseller accounts
         *
         * <param>clientId The client ID you are creating the cookie for, must be a client belonging to your reseller account</param>
         * <param>stringusername The belonging to the client ID you are logging in with the cookie</param>
         * <param>time Cookie timeout in seconds</param>
         * <returns>object Cookie value to be set in CAMPAIGNS_SSO_COOKIE</returns>
         */
        public object Integration_Get_Cookie(int clientId, string username, int time)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("clientId", clientId);
            args.Add("username", username);
            args.Add("time", time);

            return transport.MakeCall("Integration_Get_Cookie", args);
        }


        /**
         * <summary>Integrate mail2 into your own web application by setting your CAMPAIGNS_SSO_COOKIE using your client's credentials</summary>
         *
         * Please contact mail2 support for details, this function is only available to reseller accounts
         *
         * <param>stringusername The of the client</param>
         * <param>stringpassword The current of the client</param>
         * <param>time Cookie timeout in seconds</param>
         * <returns>object Returns a struct on a valid username/password combo</returns>
         */
        public object Integration_Login_Get_Cookie(string username, string password, int time)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("username", username);
            args.Add("password", password);
            args.Add("time", time);

            return transport.MakeCall("Integration_Login_Get_Cookie", args);
        }


        /**
         * <summary>Add a single email address - does not support Custom Fields</summary>
         *
         * If you need to add a single contact with custom fields, use Contact_Add
         *
         * <param>stringemail An address</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns true on success</returns>
         */
        public object Contact_Add_Email(string email, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Add_Email", args);
        }


        /**
         * <summary>Add a contact with custom fields</summary>
         *
         * <structnotes customFields>
         * customFields is a struct which can contain:
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * Ex:
         * customFields = {'first_name':'Test','last_name':'User'}
         * 
         * or
         * 
         * customFields = {1:'Test',2:'User'}
         * </structnotes customFields>
         *
         * <param>stringemail Email address of the contact</param>
         * <param>Dictionary<string, string>customFields is a container for custom field data</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns true on success</returns>
         */
        public object Contact_Add(string email, Dictionary<string, string> customFields, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);
            args.Add("customFields", customFields);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Add", args);
        }


        /**
         * <summary>Add multiple email addresses - does not support Custom Fields</summary>
         *
         * To add multiple with custom fields, use Contact_Add_Multiple
         *
         * <param>List<object>emails An array of email addresses</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a list of email addresses, each marked "true" or "false" showing whether they were suppressed</returns>
         */
        public object Contact_Add_Email_Multiple(List<object> emails, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("emails", emails);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Add_Email_Multiple", args);
        }


        /**
         * <summary>Add multiple contacts with Custom Fields</summary>
         *
         * <structnotes contacts>
         * contacts is an array of structs, where each struct can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * Each contact struct must have a valid email
         * 
         * Ex:
         * contacts = [
         * {'email':'test@mail2.com', 'first_name':'Test', 'last_name':'User'},
         * {'email':'support@mail2.com', 1:'Support', 2:'User'}
         * ];
         * 
         * Ex:
         * contacts = [
         * {'email':'test@mail2.com'},
         * {'email':'support@mail2.com'}
         * ];
         * </structnotes contacts>
         *
         * <param>List<Dictionary<string, string> >contacts Array of contact structs</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Aggregate import results</returns>
         */
        public object Contact_Add_Multiple(List<Dictionary<string, string>> contacts, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("contacts", contacts);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Add_Multiple", args);
        }


        /**
         * <summary>Import a collection of contacts.  Can import up to 1000 contacts with a single call.</summary>
         *
         * This call can be used to import new contacts or update existing contacts.
         * 
         * <structnotes contacts>
         * contacts is an array of structs, where each struct can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * Each contact_hash must have a valid email
         * 
         * Ex:
         * contacts = [
         * {'email':'test@mail2.com', 'first_name':'Test', 'last_name':'User'},
         * {'email':'support@mail2.com', 1:'Support', 2:'User'}
         * ];
         * </structnotes contacts>
         *
         * <param>List<Dictionary<string, string> >contacts Array of contact_hash items, as explained above</param>
         * <param>stringsource A short description of the of your contacts</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Aggregate import results</returns>
         */
        public object Contact_Import(List<Dictionary<string, string>> contacts, string source, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("contacts", contacts);
            args.Add("source", source);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Import", args);
        }


        /**
         * <summary>Import a collection of contacts into a set of lists and groups asynchronously</summary>
         *
         * This call is a companion to Contact_Import, but it's designed to integrate with webapps - using this call, you can get very fast responses from the API so that your webapp can continue its user-flow without waiting for all of your contacts to be verified and imported.
         * 
         * We'll send the return values to use via a POST to your callbackUrl which will include the jobId, chunkNum and a data struct containing the results of the import following the same format as the return value of Contact_Import
         * 
         * <structnotes contacts>
         * contacts is an array of structs, where each struct can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * Each contact_hash must have a valid email
         * 
         * Ex:
         * contacts = [
         * {'email':'test@mail2.com', 'first_name':'Test', 'last_name':'User'},
         * {'email':'support@mail2.com', 1:'Support', 2:'User'}
         * ];
         * </structnotes contacts>
         *
         * <param>List<Dictionary<string, string> >contacts Array of contact_hash items, as explained above</param>
         * <param>stringsource A short description of the of your contacts</param>
         * <param>stringcallbackUrl A URL endpoint for the results of the import to be POSTed to</param>
         * <param>stringjobId A Job ID used to match up the import in your webapp</param>
         * <param>chunkNum An Import Chunk number used to match up in your webapp - use this to keep track of what chunks have been processed, they may not be handled in order</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object </returns>
         */
        public object Contact_Import_Delayed(List<Dictionary<string, string>> contacts, string source, string callbackUrl, string jobId, int chunkNum, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("contacts", contacts);
            args.Add("source", source);
            args.Add("callbackUrl", callbackUrl);
            args.Add("jobId", jobId);
            args.Add("chunkNum", chunkNum);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Import_Delayed", args);
        }


        /**
         * <summary>Get a count of active contacts</summary>
         *
         * 
         *
         * <returns>object Returns the number of active contacts for your account</returns>
         */
        public object Contact_Get_Active_Count()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Contact_Get_Active_Count", args);
        }


        /**
         * <summary>Get a list of active contacts</summary>
         *
         * 
         *
         * <returns>object Returns an array containing the email addresses of active contacts</returns>
         */
        public object Contact_Get_Active()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Contact_Get_Active", args);
        }


        /**
         * <summary>Get information on a single contact</summary>
         *
         * 
         *
         * <param>stringemail The address of the contact you want information for</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct of structs, keyed off of email address, each containing the keys specified above</returns>
         */
        public object Contact_Get(string email, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Get", args);
        }


        /**
         * <summary>Get a list of contacts</summary>
         *
         * score and rating are only returned if one or more of these optionalParameters: scoreMin, scoreMax, ratingMin, or ratingMax is provided.
         *
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct of structs, keyed off of email address, each containing the keys specified above</returns>
         */
        public object Contact_Find(Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Find", args);
        }


        /**
         * <summary>Update the custom fields of an existing contact</summary>
         *
         * Will not update the custom fields for a contact that is deactivated (bounced, globally unsubscribed, etc.)
         * 
         * <structnotes customFields>
         * customFields is an array which can contain:
         *  - customFieldToken => value
         *  - customFieldId => value
         * </structnotes customFields>
         *
         * <param>stringemail The address of the contact you want to update</param>
         * <param>Dictionary<string, string>customFields The custom fields you want to update and their new values</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns true on success</returns>
         */
        public object Contact_Update(string email, Dictionary<string, string> customFields, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);
            args.Add("customFields", customFields);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Update", args);
        }


        /**
         * <summary>Change the email address for an existing contact while preserving list and group subscriptions</summary>
         *
         * Once you change the contact's email address, you will need to use the new email address to access the contact
         *
         * <param>stringemail The current address of the contact</param>
         * <param>stringnewEmail The contact's new email address</param>
         * <returns>object Returns a struct of structs, keyed off of email address, each containing the keys specified below</returns>
         */
        public object Contact_Change_Email(string email, string newEmail)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);
            args.Add("newEmail", newEmail);

            return transport.MakeCall("Contact_Change_Email", args);
        }


        /**
         * <summary>Return a contact to active status</summary>
         *
         * 
         *
         * <param>stringemail The address of the contact you wish to activate</param>
         * <returns>object Returns true on success</returns>
         */
        public object Contact_Activate(string email)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);

            return transport.MakeCall("Contact_Activate", args);
        }


        /**
         * <summary>Delete a contact</summary>
         *
         * 
         *
         * <param>stringemail An address</param>
         * <returns>object True if delete was successful</returns>
         */
        public object Contact_Delete(string email)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);

            return transport.MakeCall("Contact_Delete", args);
        }


        /**
         * <summary>Suppress a contact</summary>
         *
         * 
         *
         * <param>stringemail An address</param>
         * <returns>object Returns true or false indicating whether the contact was suppressed</returns>
         */
        public object Contact_Suppress(string email)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);

            return transport.MakeCall("Contact_Suppress", args);
        }


        /**
         * <summary>Suppress multiple contacts</summary>
         *
         * 
         *
         * <param>List<object>emails An array of email addresses</param>
         * <returns>object Returns a list of email addresses, each marked "true" or "false" showing whether they were suppressed</returns>
         */
        public object Contact_Suppress_Multiple(List<object> emails)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("emails", emails);

            return transport.MakeCall("Contact_Suppress_Multiple", args);
        }


        /**
         * <summary>Purge a contact</summary>
         *
         * 
         *
         * <param>stringemail An address</param>
         * <returns>object Returns true or false indicating whether the contact was purged</returns>
         */
        public object Contact_Purge(string email)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);

            return transport.MakeCall("Contact_Purge", args);
        }


        /**
         * <summary>Get the history for a contact</summary>
         *
         * All contact history data is returned by default
         * 
         * <structnotes optionalParameters>
         * Format for minDate and maxDate:
         *  - A specified date/time in the format: YYYY-MM-DD HH:MM:SS
         *  - A relative time string including, but not limited to:
         *    * 'now'
         *    * 'today'
         *    * 'tomorrow'
         *    * 'first day of January 2010' (example)
         *    * 'last day of March 2010' (example)
         *    * 'Monday this week' (example)
         *    * 'Tuesday next week' (example)
         * 
         * - Values allowed in the historyTypes:
         *    * click
         *    * open
         *    * send
         *    * status
         *    * customFields
         *    * subscription
         *    * bounce
         * </structnotes optionalParameters>
         *
         * <param>stringemail An address</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct of history information</returns>
         */
        public object Contact_Get_History(string email, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Get_History", args);
        }


        /**
         * <summary>Get the history for a set of contacts</summary>
         *
         * All contact history data is returned by default
         * 
         * <structnotes optionalParameters>
         * Format for minDate and maxDate:
         *  - A specified date/time in the format: YYYY-MM-DD HH:MM:SS
         *  - A relative time string including, but not limited to:
         *    * 'now'
         *    * 'today'
         *    * 'tomorrow'
         *    * 'first day of January 2010' (example)
         *    * 'last day of March 2010' (example)
         *    * 'Monday this week' (example)
         *    * 'Tuesday next week' (example)
         * 
         * - Values allowed in the historyTypes:
         *    * click
         *    * open
         *    * send
         *    * status
         *    * customFields
         *    * subscription
         *    * bounce
         * </structnotes optionalParameters>
         *
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct of history information</returns>
         */
        public object Contact_Get_History_Multiple(Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Get_History_Multiple", args);
        }


        /**
         * <summary>Get the listIds for all the lists this contact is subscribed to</summary>
         *
         * 
         *
         * <param>stringemail An address</param>
         * <returns>object Returns an array of listIds</returns>
         */
        public object Contact_Get_Subscriptions(string email)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);

            return transport.MakeCall("Contact_Get_Subscriptions", args);
        }


        /**
         * <summary>Unsubscribe a contact from all lists, then subscribe the contact to the
      specified lists.</summary>
         *
         * 
         * After this function is finished, the contact will only be subscribed to
         *   the lists specified in listIds
         *
         * <param>stringemail The address of the contact you wish to set the subscriptions of</param>
         * <param>List<object>listIds An array of the contact should be subscribed to and unsubscribed from all others</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object An array of all the lists the contact is subscribed to after the operation, should have the same values as listIds</returns>
         */
        public object Contact_Set_Subscriptions(string email, List<object> listIds, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);
            args.Add("listIds", listIds);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Contact_Set_Subscriptions", args);
        }


        /**
         * <summary>Get a list of all email addresses that are not subscribed to any list</summary>
         *
         * 
         *
         * <returns>object A list of email addresses that are not subscribed to any list</returns>
         */
        public object Contact_Get_No_Subscriptions()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Contact_Get_No_Subscriptions", args);
        }


        /**
         * <summary>Get a list of all email addresses that have been sent campaigns but have not opened or clicked</summary>
         *
         * 
         *
         * <returns>object A list of email addresses have been sent campaigns but have not opened or clicked</returns>
         */
        public object Contact_Get_No_Activity()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Contact_Get_No_Activity", args);
        }


        /**
         * <summary>Delete all email addresses that have been sent campaigns but have not opened or clicked</summary>
         *
         * 
         *
         * <returns>object The number of contacts that were deleted</returns>
         */
        public object Contact_Delete_No_Activity()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Contact_Delete_No_Activity", args);
        }


        /**
         * <summary>Retrieve the Person Code for a given contact</summary>
         *
         * A Contact Person Code is often used in tracking components, link tracking, open tracking, reply tracking
         *
         * <param>stringemail The contact's address you want the Person Code for</param>
         * <returns>object Person Code</returns>
         */
        public object Contact_Get_Person_Code(string email)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);

            return transport.MakeCall("Contact_Get_Person_Code", args);
        }


        /**
         * <summary>Delete all email addresses that are not subscribed to any list</summary>
         *
         * 
         *
         * <returns>object The number of contacts that were deleted</returns>
         */
        public object Contact_Delete_No_Subscriptions()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Contact_Delete_No_Subscriptions", args);
        }


        /**
         * <summary>Add a Textbox CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_Textbox(string fieldName, bool required, bool subscriberCanEdit, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_Textbox", args);
        }


        /**
         * <summary>Add a Decimal CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_Decimal(string fieldName, bool required, bool subscriberCanEdit, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_Decimal", args);
        }


        /**
         * <summary>Add an Integer CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_Integer(string fieldName, bool required, bool subscriberCanEdit, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_Integer", args);
        }


        /**
         * <summary>Add a Dropdown CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>List<object>options An array of strings to be shown in the dropdown</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_Dropdown(string fieldName, bool required, bool subscriberCanEdit, List<object> options, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("options", options);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_Dropdown", args);
        }


        /**
         * <summary>Add a Radio CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>List<object>options An array of strings, each value will have its own radio button</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_Radio(string fieldName, bool required, bool subscriberCanEdit, List<object> options, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("options", options);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_Radio", args);
        }


        /**
         * <summary>Add a single Checkbox CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_Checkbox(string fieldName, bool required, bool subscriberCanEdit, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_Checkbox", args);
        }


        /**
         * <summary>Add a CheckboxList CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>List<object>options An array of strings, each value will have its own checkbox</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_CheckboxList(string fieldName, bool required, bool subscriberCanEdit, List<object> options, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("options", options);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_CheckboxList", args);
        }


        /**
         * <summary>Add a Date CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_Date(string fieldName, bool required, bool subscriberCanEdit, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_Date", args);
        }


        /**
         * <summary>Add an Email CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_Email(string fieldName, bool required, bool subscriberCanEdit, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_Email", args);
        }


        /**
         * <summary>Add a Phone CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_Phone(string fieldName, bool required, bool subscriberCanEdit, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_Phone", args);
        }


        /**
         * <summary>Add a StateDropdown CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_StateDropdown(string fieldName, bool required, bool subscriberCanEdit, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_StateDropdown", args);
        }


        /**
         * <summary>Add an Address CustomField to your signup form</summary>
         *
         * The CustomField token will be automatically generated based on the fieldName you provide
         *
         * <param>stringfieldName The name of your CustomField - this will be the label for your field on the form</param>
         * <param>boolrequired Is this field when the form is filled out?</param>
         * <param>boolsubscriberCanEdit Can the subscriber edit this value later?</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing the new CustomFields fieldId and token</returns>
         */
        public object CustomField_Add_Address(string fieldName, bool required, bool subscriberCanEdit, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldName", fieldName);
            args.Add("required", required);
            args.Add("subscriberCanEdit", subscriberCanEdit);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("CustomField_Add_Address", args);
        }


        /**
         * <summary>Get a list of CustomFields - excluding searchParameters indicates you want a list of all CustomFields</summary>
         *
         * 
         *
         * <param>Dictionary<string, string>searchParameters Provide a struct to narrow your results</param>
         * <returns>object Returns a struct for each CustomField found</returns>
         */
        public object CustomField_Find(Dictionary<string, string> searchParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("searchParameters", searchParameters);

            return transport.MakeCall("CustomField_Find", args);
        }


        /**
         * <summary>Get a list of all current CustomFields</summary>
         *
         * 
         *
         * <returns>object Returns a struct for each CustomField found</returns>
         */
        public object CustomField_Get_All()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("CustomField_Get_All", args);
        }


        /**
         * <summary>Update an existing CustomField</summary>
         *
         * 
         *
         * <param>fieldId The of the CustomField you want to modify</param>
         * <param>Dictionary<string, string>updateParameters A struct of replacement values for your CustomField - only specify fields that you want to change</param>
         * <returns>object Returns a struct with your CustomField's new properties</returns>
         */
        public object CustomField_Update(int fieldId, Dictionary<string, string> updateParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldId", fieldId);
            args.Add("updateParameters", updateParameters);

            return transport.MakeCall("CustomField_Update", args);
        }


        /**
         * <summary>Update the displayOrder property on multiple fields at once</summary>
         *
         * 
         *
         * <param>Dictionary<string, string>reorder A struct where each key is a fieldId and each value is the new displayOrder</param>
         * <returns>object Returns a struct for each CustomField updated</returns>
         */
        public object CustomField_Reorder(Dictionary<string, string> reorder)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("reorder", reorder);

            return transport.MakeCall("CustomField_Reorder", args);
        }


        /**
         * <summary>Delete a CustomField - this action cannot be undone</summary>
         *
         * 
         *
         * <param>fieldId The of the CustomField you want to delete - first_name, last_name and email_address cannot be deleted</param>
         * <returns>object Returns true on success</returns>
         */
        public object CustomField_Delete(int fieldId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fieldId", fieldId);

            return transport.MakeCall("CustomField_Delete", args);
        }


        /**
         * <summary>Create a new test contact list</summary>
         *
         * A Test Contact List is a special Private list that can be sent test campaigns
         *
         * <param>stringname The of your list</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object id Returns the List ID of your new List</returns>
         */
        public object List_Add_Test(string name, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("name", name);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("List_Add_Test", args);
        }


        /**
         * <summary>Create a new internal contact list</summary>
         *
         * Contacts will not see this list if they edit their profile nor will they be able to subscribe to this list from the subscription center. If an internal list member unsubscribes (via link in a message), they will be Globally Unsubscribed from your database.
         *
         * <param>stringname The of your list</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object id Returns the List ID of your new List</returns>
         */
        public object List_Add_Internal(string name, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("name", name);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("List_Add_Internal", args);
        }


        /**
         * <summary>Create a new private contact list</summary>
         *
         * Choose this option if your contacts must be 'authorized' before they can be on the list. Private lists are not publicly available to sign up for, but will be seen by recipients in the subscription center if they modify their contact information. Example: Alumni lists
         * 
         * 
         * <structnotes optInMesage>
         * Example optInMessage:
         * 
         * <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
         * <html>
         * <head>
         * <title>Message</title>
         * </head>
         * <body style="font-family: Times New Roman;font-size: 12px;">
         * <p>Dear Customer,</p>
         * <p>Thank you for subscribing to our list.  To get started, please <a href="{confirm_url}">click here</a> to confirm your subscription.</p>
         * <p>Thank you,<br />Unit Test Team</p>
         * </body>
         * </html>
         * </structnotes optInMesage>
         *
         * <param>stringname The of your list</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object id Returns the List ID of your new List</returns>
         */
        public object List_Add_Private(string name, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("name", name);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("List_Add_Private", args);
        }


        /**
         * <summary>Create a new public contact list</summary>
         *
         * Choose this option if you want to allow anyone to sign up for this list via your website. Example: Newsletters
         * 
         * 
         * <structnotes optInMesage>
         * Example optInMessage:
         * 
         * <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
         * <html>
         * <head>
         * <title>Message</title>
         * </head>
         * <body style="font-family: Times New Roman;font-size: 12px;">
         * <p>Dear Customer,</p>
         * <p>Thank you for subscribing to our list.  To get started, please <a href="{confirm_url}">click here</a> to confirm your subscription.</p>
         * <p>Thank you,<br />Unit Test Team</p>
         * </body>
         * </html>
         * </structnotes optInMesage>
         *
         * <param>stringname The of your list</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object id Returns the List ID of your new List</returns>
         */
        public object List_Add_Public(string name, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("name", name);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("List_Add_Public", args);
        }


        /**
         * <summary>Delete a list you created</summary>
         *
         * 
         *
         * <param>listId The List ID of the list you wish to delete</param>
         * <returns>object Returns true on success</returns>
         */
        public object List_Delete(int listId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);

            return transport.MakeCall("List_Delete", args);
        }


        /**
         * <summary>Add an email address contact to an existing list</summary>
         *
         * 
         *
         * <param>listId The ID of the list you want to subscribe the email address to</param>
         * <param>stringemail The address you are subscribing</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object True on success</returns>
         */
        public object List_Subscribe(int listId, string email, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("email", email);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("List_Subscribe", args);
        }


        /**
         * <summary>Remove an email address contact from an existing list</summary>
         *
         * 
         *
         * <param>listId The ID of the list you want to unsubscribe the email address from</param>
         * <param>stringemail The address you are unsubscribing</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object True on success</returns>
         */
        public object List_Unsubscribe(int listId, string email, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("email", email);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("List_Unsubscribe", args);
        }


        /**
         * <summary>Remove multiple email address contacts from an existing list</summary>
         *
         * 
         *
         * <param>listId The ID of the list you want to unsubscribe the email address from</param>
         * <param>List<object>emails An array of email addresses you are unsubscribing</param>
         * <returns>object Returns the number of contacts removed</returns>
         */
        public object List_Unsubscribe_Multiple(int listId, List<object> emails)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("emails", emails);

            return transport.MakeCall("List_Unsubscribe_Multiple", args);
        }


        /**
         * <summary>Retrieve a list of contacts in a given list</summary>
         *
         * 
         *
         * <param>listId The ID of the list you retrieve contacts from</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Struct of records each with a key of email and values of contactId, email, status, statusCode and listStatus</returns>
         */
        public object List_Get_Contacts(int listId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("List_Get_Contacts", args);
        }


        /**
         * <summary>Get a listing of currently active lists</summary>
         *
         * 
         *
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Array of records with the key of listId and values of listId, name, description and type</returns>
         */
        public object List_Get_Active_Lists(Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("List_Get_Active_Lists", args);
        }


        /**
         * <summary>Get information about a list, including name, type, easyCast, listOwner and optIn info (where applicable)</summary>
         *
         * 
         *
         * <param>listId The ID of the list you are getting info for</param>
         * <returns>object Returns info about the requested list</returns>
         */
        public object List_Get_Info(int listId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);

            return transport.MakeCall("List_Get_Info", args);
        }


        /**
         * <summary>Import a collection of contacts into a given list</summary>
         *
         * <structnotes contacts>
         * contacts is an array of structs, where each struct can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * Each contact_hash must have a valid email
         * 
         * Ex:
         * contacts = [
         * {'email':'test@mail2.com', 'first_name':'Test', 'last_name':'User'},
         * {'email':'support@mail2.com', 1:'Support', 2:'User'}
         * ];
         * </structnotes contacts>
         *
         * <param>listId The ID of the list you are importing contacts into</param>
         * <param>stringsource A short description of the of your contacts</param>
         * <param>List<Dictionary<string, string> >contacts Array of contact_hash items, as explained above</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Aggregate import results</returns>
         */
        public object List_Import_Contacts(int listId, string source, List<Dictionary<string, string>> contacts, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("source", source);
            args.Add("contacts", contacts);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("List_Import_Contacts", args);
        }


        /**
         * <summary>Import a collection of contacts into a given list asyncrhonously</summary>
         *
         * This call is a companion to List_Import_Contacts, but it's designed to integrate with webapps - using this call, you can get very fast responses from the API so that your webapp can continue its user-flow without waiting for all of your contacts to be verified and imported.
         * 
         * We'll send the return values to use via a POST to your callbackUrl which will include the jobId, chunkNum and a data struct containing the results of the import following the same format as the return value of List_Import_Contacts
         * 
         * <structnotes contacts>
         * contacts is an array of structs, where each struct can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * Each contact_hash must have a valid email
         * 
         * Ex:
         * contacts = [
         * {'email':'test@mail2.com', 'first_name':'Test', 'last_name':'User'},
         * {'email':'support@mail2.com', 1:'Support', 2:'User'}
         * ];
         * </structnotes contacts>
         *
         * <param>listId The ID of the list you are importing contacts into</param>
         * <param>stringsource A short description of the of your contacts</param>
         * <param>List<Dictionary<string, string> >contacts Array of contact_hash items, as explained above</param>
         * <param>stringcallbackUrl A URL endpoint for the results of the import to be POSTed to</param>
         * <param>stringjobId A Job ID used to match up the import in your webapp</param>
         * <param>chunkNum An Import Chunk number used to match up in your webapp - use this to keep track of what chunks have been processed, they may not be handled in order</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object </returns>
         */
        public object List_Import_Contacts_Delayed(int listId, string source, List<Dictionary<string, string>> contacts, string callbackUrl, string jobId, int chunkNum, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("source", source);
            args.Add("contacts", contacts);
            args.Add("callbackUrl", callbackUrl);
            args.Add("jobId", jobId);
            args.Add("chunkNum", chunkNum);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("List_Import_Contacts_Delayed", args);
        }


        /**
         * <summary>Evaluate a collection of contacts</summary>
         *
         * This function works exactly like List_Import_Contacts, but will not actually insert or modify any contacts.  This function can be used to evaluate a large list of contacts to find problematic entries before performing an actual insert.
         * 
         * Batches of 50,000 contacts can be evaluated
         * 
         * <structnotes contacts>
         * contacts is an array of structs, where each struct can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * Each contact_hash must have a valid email
         * 
         * Ex:
         * contacts = [
         * {'email':'test@mail2.com', 'first_name':'Test', 'last_name':'User'},
         * {'email':'support@mail2.com', 1:'Support', 2:'User'}
         * ];
         * </structnotes contacts>
         *
         * <param>List<Dictionary<string, string> >contacts Array of contact_hash items, as explained above</param>
         * <returns>object Aggregate import results</returns>
         */
        public object List_Evaluate_Contacts(List<Dictionary<string, string>> contacts)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("contacts", contacts);

            return transport.MakeCall("List_Evaluate_Contacts", args);
        }


        /**
         * <summary>Get the number of contacts in a given list</summary>
         *
         * 
         *
         * <param>listId The ID of the list you want the count from</param>
         * <param>stringstatus Count only contacts with a particular list status, valid values: subscribed, unsubscribed, bounced (defaults to subscribed)</param>
         * <returns>object Number of contacts in list</returns>
         */
        public object List_Get_Count(int listId, string status)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("status", status);

            return transport.MakeCall("List_Get_Count", args);
        }


        /**
         * <summary>Update an existing Test List</summary>
         *
         * Update Parameters can include:
         *  - name: The name of your list
         *  - description:  A short description for your list
         *  - easycastName: A one word shortcut for EasyCast access, will create an EasyCast email address in the format NAME.list.YOURID@send.emailcampaigns.net
         *  - listOwnerEmail: An email address that receives an email whenever a contact subscribes to this list, and can approve "EasyCast" messages
         *
         * <param>listId The of the list you want to modify</param>
         * <param>Dictionary<string, string>updateParameters A struct of replacement values for your list - only specify items that you want to change</param>
         * <returns>object Returns true on success</returns>
         */
        public object List_Update_Test(int listId, Dictionary<string, string> updateParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("updateParameters", updateParameters);

            return transport.MakeCall("List_Update_Test", args);
        }


        /**
         * <summary>Update an existing Internal List</summary>
         *
         * Update Parameters can include:
         *  - name: The name of your list
         *  - description:  A short description for your list
         *  - easycastName: A one word shortcut for EasyCast access, will create an EasyCast email address in the format NAME.list.YOURID@send.emailcampaigns.net
         *  - listOwnerEmail: An email address that receives an email whenever a contact subscribes to this list, and can approve "EasyCast" messages
         *
         * <param>listId The of the list you want to modify</param>
         * <param>Dictionary<string, string>updateParameters A struct of replacement values for your list - only specify items that you want to change</param>
         * <returns>object Returns true on success</returns>
         */
        public object List_Update_Internal(int listId, Dictionary<string, string> updateParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("updateParameters", updateParameters);

            return transport.MakeCall("List_Update_Internal", args);
        }


        /**
         * <summary>Update an existing Private List</summary>
         *
         * Update Parameters can include:
         *  - string description A short description for your list
         *  - string easycastName A one word shortcut for EasyCast access, will create an EasyCast email address in the format NAME.list.YOURID@send.emailcampaigns.net
         *  - string listOwnerEmail An email address that receives an email whenever a contact subscribes to this list, and can approve "EasyCast" messages
         *  - bool optIn Setting this to true means that the system will send contacts a confirmation email before sending them messages
         *  - string optInFromEmail The email address that the optInMessage confirmation email will be sent from.  Required if optIn is true
         *  - string optInFromEmailAlias The From Name that the optInMessage confirmation email will be sent from.
         *  - string optInSubject The subject line of the optInMessage confirmation email.  Required if optIn is true
         *  - string optInMessage The HTML email body for the confirmation email. MUST include the {confirm_url} token that will insert the link to the correct subscription confirmation page.  Required if optIn is true
         * 
         * Example optInMessage:
         * 
         * <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
         * <html>
         * <head>
         * <title>Message</title>
         * </head>
         * <body style="font-family: Times New Roman;font-size: 12px;">
         * <p>Dear Customer,</p>
         * <p>Thank you for subscribing to our list.  To get started, please <a href="{confirm_url}">click here</a> to confirm your subscription.</p>
         * <p>Thank you,<br />Unit Test Team</p>
         * </body>
         * </html>
         *
         * <param>listId The of the list you want to modify</param>
         * <param>Dictionary<string, string>updateParameters A struct of replacement values for your list - only specify items that you want to change</param>
         * <returns>object Returns true on success</returns>
         */
        public object List_Update_Private(int listId, Dictionary<string, string> updateParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("updateParameters", updateParameters);

            return transport.MakeCall("List_Update_Private", args);
        }


        /**
         * <summary>Update an existing Public List</summary>
         *
         * Update Parameters can include:
         *  - string description A short description for your list
         *  - string easycastName A one word shortcut for EasyCast access, will create an EasyCast email address in the format NAME.list.YOURID@send.emailcampaigns.net
         *  - string listOwnerEmail An email address that receives an email whenever a contact subscribes to this list, and can approve "EasyCast" messages
         *  - bool optIn Setting this to true means that the system will send contacts a confirmation email before sending them messages
         *  - string optInFromEmail The email address that the optInMessage confirmation email will be sent from.  Required if optIn is true
         *  - string optInFromEmailAlias The From Name that the optInMessage confirmation email will be sent from.
         *  - string optInSubject The subject line of the optInMessage confirmation email.  Required if optIn is true
         *  - string optInMessage The HTML email body for the confirmation email. MUST include the {confirm_url} token that will insert the link to the correct subscription confirmation page.  Required if optIn is true
         * 
         * Example optInMessage:
         * 
         * <!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
         * <html>
         * <head>
         * <title>Message</title>
         * </head>
         * <body style="font-family: Times New Roman;font-size: 12px;">
         * <p>Dear Customer,</p>
         * <p>Thank you for subscribing to our list.  To get started, please <a href="{confirm_url}">click here</a> to confirm your subscription.</p>
         * <p>Thank you,<br />Unit Test Team</p>
         * </body>
         * </html>
         *
         * <param>listId The of the list you want to modify</param>
         * <param>Dictionary<string, string>updateParameters A struct of replacement values for your list - only specify items that you want to change</param>
         * <returns>object Returns true on success</returns>
         */
        public object List_Update_Public(int listId, Dictionary<string, string> updateParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("updateParameters", updateParameters);

            return transport.MakeCall("List_Update_Public", args);
        }


        /**
         * <summary>Create a new Group</summary>
         *
         * Contact groups are simple collections of contacts.  They do not have subscription information or appear in the subscription center like a list, but you can add and remove contacts from groups through imports and quick adds.  The purpose of contact groups is to provide a simple way of grouping your contacts together, without the added complexity of list subscriptions.  Because groups are so simple, the only information you need to create a group is a name.
         *
         * <param>stringname The of your group</param>
         * <returns>object id Returns the Group ID of your new Group</returns>
         */
        public object Group_Create(string name)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("name", name);

            return transport.MakeCall("Group_Create", args);
        }


        /**
         * <summary>Update an existing group</summary>
         *
         * 
         *
         * <param>groupId The of the group you want to modify</param>
         * <param>stringname The new of your group</param>
         * <returns>object Returns true on success</returns>
         */
        public object Group_Update(int groupId, string name)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("groupId", groupId);
            args.Add("name", name);

            return transport.MakeCall("Group_Update", args);
        }


        /**
         * <summary>Delete a group you created</summary>
         *
         * 
         *
         * <param>groupId The Group ID of the group you wish to delete</param>
         * <returns>object Returns true on success</returns>
         */
        public object Group_Delete(int groupId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("groupId", groupId);

            return transport.MakeCall("Group_Delete", args);
        }


        /**
         * <summary>Add an email address contact to an existing group</summary>
         *
         * 
         *
         * <param>groupId The ID of the group you want to subscribe the email address to</param>
         * <param>stringemail The address you are subscribing</param>
         * <returns>object True on success</returns>
         */
        public object Group_Add_Contact(int groupId, string email)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("groupId", groupId);
            args.Add("email", email);

            return transport.MakeCall("Group_Add_Contact", args);
        }


        /**
         * <summary>Remove an email address contact from an existing group</summary>
         *
         * 
         *
         * <param>groupId The ID of the group you want to unsubscribe the email address to</param>
         * <param>stringemail The address you are unsubscribing</param>
         * <returns>object True on success</returns>
         */
        public object Group_Remove_Contact(int groupId, string email)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("groupId", groupId);
            args.Add("email", email);

            return transport.MakeCall("Group_Remove_Contact", args);
        }


        /**
         * <summary>Remove multiple email address contacts from an existing group</summary>
         *
         * 
         *
         * <param>groupId The ID of the group you want to remove the email address from</param>
         * <param>List<object>emails An array of email addresses you are removing from the group</param>
         * <returns>object Returns the number of contacts removed</returns>
         */
        public object Group_Remove_Contacts_Multiple(int groupId, List<object> emails)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("groupId", groupId);
            args.Add("emails", emails);

            return transport.MakeCall("Group_Remove_Contacts_Multiple", args);
        }


        /**
         * <summary>Retrieve a list of contacts in a given group</summary>
         *
         * 
         *
         * <param>groupId The ID of the group you retrieve contacts from</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Struct of records each with a key of the contact's email addressemail and values of contactId, email, status and statusCode</returns>
         */
        public object Group_Get_Contacts(int groupId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("groupId", groupId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Group_Get_Contacts", args);
        }


        /**
         * <summary>Get a listing of currently active groups</summary>
         *
         * 
         *
         * <returns>object Struct of records with the key of groupId and value of name</returns>
         */
        public object Group_List()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Group_List", args);
        }


        /**
         * <summary>Import a collection of contacts into a given group</summary>
         *
         * <structnotes contacts>
         * contacts is an array of associative arrays, where each associative array can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * Each contact must have a valid email
         * 
         * Ex:
         * contacts = [
         * {'email':'test@mail2.com', 'first_name':'Test', 'last_name':'User'},
         * {'email':'support@mail2.com', 1:'Support', 2:'User'}
         * ];
         * </structnotes contacts>
         *
         * <param>groupId The ID of the group you are importing contacts into</param>
         * <param>stringsource A short description of the of your contacts</param>
         * <param>List<Dictionary<string, string> >contacts Array of contact_hash items, as explained above</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Aggregate import results</returns>
         */
        public object Group_Import_Contacts(int groupId, string source, List<Dictionary<string, string>> contacts, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("groupId", groupId);
            args.Add("source", source);
            args.Add("contacts", contacts);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Group_Import_Contacts", args);
        }


        /**
         * <summary>Get the number of contacts in a given group</summary>
         *
         * 
         *
         * <param>groupId The ID of the group you want the count from</param>
         * <returns>object Number of contacts in group</returns>
         */
        public object Group_Get_Count(int groupId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("groupId", groupId);

            return transport.MakeCall("Group_Get_Count", args);
        }


        /**
         * <summary>Create a new SavedSearch</summary>
         *
         * 
         *
         * <param>stringname A for your saved search, must be unique</param>
         * <param>List<object>advancedConditions An array of AdvancedCondition items - see AdvancedCondition for more info</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns the searchId of your new search</returns>
         */
        public object SavedSearch_Create(string name, List<object> advancedConditions, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("name", name);
            args.Add("advancedConditions", advancedConditions);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("SavedSearch_Create", args);
        }


        /**
         * <summary>Delete a savedSearch you created</summary>
         *
         * 
         *
         * <param>searchId The SavedSearch ID of the savedSearch you wish to delete</param>
         * <returns>object Returns true on success</returns>
         */
        public object SavedSearch_Delete(int searchId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("searchId", searchId);

            return transport.MakeCall("SavedSearch_Delete", args);
        }


        /**
         * <summary>Retrieve a list of contacts found by a given saved search</summary>
         *
         * 
         *
         * <param>searchId The ID of the savedSearch you retrieve contacts from</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Struct of records each with a key of email and values of contactId, email, status and statusCode</returns>
         */
        public object SavedSearch_Get_Contacts(int searchId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("searchId", searchId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("SavedSearch_Get_Contacts", args);
        }


        /**
         * <summary>Get a listing of Saved Searches</summary>
         *
         * 
         *
         * <returns>object Struct of records with the key of searchId and value of name</returns>
         */
        public object SavedSearch_List()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("SavedSearch_List", args);
        }


        /**
         * <summary>Get the number of contacts in a given savedSearch</summary>
         *
         * 
         *
         * <param>searchId The ID of the savedSearch you want the count from</param>
         * <returns>object Number of contacts in savedSearch</returns>
         */
        public object SavedSearch_Get_Count(int searchId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("searchId", searchId);

            return transport.MakeCall("SavedSearch_Get_Count", args);
        }


        /**
         * <summary>Create a standard mail2 campaign</summary>
         *
         * 
         * <structnotes recipients>
         * Any combination of the above can be provided in recipients.
         * Ex. 1:{'recipients':{'list':3,group:[1,2,3],'search':[2,3]}
         * Ex. 2:{'recipients':{'list':[1,2],group:1,'search':[1,2]}
         * Ex. 3:{'recipients':{'list':[1,2],group:[2,3],'search':1}
         * Ex. 4:{'recipients':{'list':1}
         * </structnotes recipients>
         * 
         * <structnotes content>
         * At least one of html or text must be provided.  If both are provided, the message will be sent as a multipart message
         * </structnotes content>
         * 
         * 
         * 
         * <structnotes exclusions>
         * Any combination of the above can be provided in exclusions.
         * Ex. 1:{'exclusions':{'list':3,group:[1,2,3],'search':[2,3]}
         * Ex. 2:{'exclusions':{'list':[1,2],group:1,'search':[1,2]}
         * Ex. 3:{'exclusions':{'list':[1,2],group:[2,3],'search':1}
         * Ex. 4:{'exclusions':{'list':1}
         * </structnotes exclusions>
         * 
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>Dictionary<string, object>recipients A struct which specifies the for your Campaign - can include list, group and search</param>
         * <param>stringcampaignName The name of this Campaign - not shown to recipients</param>
         * <param>stringsubject The line of the Campaign</param>
         * <param>stringsenderEmail The from email address of the Campaign</param>
         * <param>stringsenderName The from name of the Campaign</param>
         * <param>Dictionary<string, string>content A struct which specifies the of the Campaign email - can include html and text</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object campaignId The ID for your new Campaign</returns>
         */
        public object Campaign_Create_Standard(Dictionary<string, object> recipients, string campaignName, string subject, string senderEmail, string senderName, Dictionary<string, string> content, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("recipients", recipients);
            args.Add("campaignName", campaignName);
            args.Add("subject", subject);
            args.Add("senderEmail", senderEmail);
            args.Add("senderName", senderName);
            args.Add("content", content);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Create_Standard", args);
        }


        /**
         * <summary>Create a standard mail2 campaign</summary>
         *
         * 
         * <structnotes recipients>
         * Any combination of the above can be provided in recipients.
         * Ex. 1:{'recipients':{'list':3,group:[1,2,3],'search':[2,3]}
         * Ex. 2:{'recipients':{'list':[1,2],group:1,'search':[1,2]}
         * Ex. 3:{'recipients':{'list':[1,2],group:[2,3],'search':1}
         * Ex. 4:{'recipients':{'list':1}
         * </structnotes recipients>
         * 
         * 
         * <structnotes exclusions>
         * Any combination of the above can be provided in exclusions.
         * Ex. 1:{'exclusions':{'list':3,group:[1,2,3],'search':[2,3]}
         * Ex. 2:{'exclusions':{'list':[1,2],group:1,'search':[1,2]}
         * Ex. 3:{'exclusions':{'list':[1,2],group:[2,3],'search':1}
         * Ex. 4:{'exclusions':{'list':1}
         * </structnotes exclusions>
         * 
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         * 
         * 
         * 
         * <structnotes content>
         * At least one of htmlUrl or textUrl must be provided.  If both are provided, the message will be sent as a multipart message
         * </structnotes content>
         *
         * <param>Dictionary<string, object>recipients A struct which specifies the for your Campaign - can include list, group and search</param>
         * <param>stringcampaignName The name of this Campaign - not shown to recipients</param>
         * <param>stringsubject The line of the Campaign</param>
         * <param>stringsenderEmail The from email address of the Campaign</param>
         * <param>stringsenderName The from name of the Campaign</param>
         * <param>Dictionary<string, string>content A struct which specifies the URLs that hold the of the Campaign email - can include htmlUrl and textUrl</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object campaignId The ID for your new Campaign</returns>
         */
        public object Campaign_Create_Standard_From_Url(Dictionary<string, object> recipients, string campaignName, string subject, string senderEmail, string senderName, Dictionary<string, string> content, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("recipients", recipients);
            args.Add("campaignName", campaignName);
            args.Add("subject", subject);
            args.Add("senderEmail", senderEmail);
            args.Add("senderName", senderName);
            args.Add("content", content);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Create_Standard_From_Url", args);
        }


        /**
         * <summary>Create a campaign to be sent to an ad hoc list of email addresses.  The campaign will send immediately, it is not necessary to call Campaign_Send</summary>
         *
         * There is a limit of 1000 contacts per call, however you can use Campaign_Add_Recipients to send more after your first batch is completed.
         * 
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         * 
         * 
         * 
         * <structnotes contacts>
         * contacts is an array of structs, where each struct can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * Each contact must have a valid email. Role-based email addresses (e.g., sales@, marketing@, postmaster@) are not permitted.
         * 
         * Ex:
         * contacts = [
         * {'email':'testuser@mail2.com', 'first_name':'Test', 'last_name':'User'},
         * {'email':'anotheruser@mail2.com', 1:'Another', 2:'User'}
         * ];
         * </structnotes contacts>
         * 
         * <structnotes content>
         * At least one of html or text must be provided.  If both are provided, the message will be sent as a multipart message
         * </structnotes content>
         *
         * <param>List<Dictionary<string, string> >contacts an array of contact items</param>
         * <param>stringcampaignName The name of this Campaign - not shown to recipients</param>
         * <param>stringsubject The line of the Campaign</param>
         * <param>stringsenderEmail The from email address of the Campaign</param>
         * <param>stringsenderName The from name of the Campaign</param>
         * <param>Dictionary<string, string>content A struct which specifies the of the Campaign email - can include html and text</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing info about the new campaign</returns>
         */
        public object Campaign_Create_Ad_Hoc(List<Dictionary<string, string>> contacts, string campaignName, string subject, string senderEmail, string senderName, Dictionary<string, string> content, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("contacts", contacts);
            args.Add("campaignName", campaignName);
            args.Add("subject", subject);
            args.Add("senderEmail", senderEmail);
            args.Add("senderName", senderName);
            args.Add("content", content);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Create_Ad_Hoc", args);
        }


        /**
         * <summary>Create an Triggered campaign that sends every time someone subscribes to a given List</summary>
         *
         * Note:  Triggered campaigns won't send until they are activated
         * 
         * timeType and timeValue work together to define when your Triggered Campaign fires, ex:
         * timeValue = 10, timeType = minutes means that your Triggered Campaign will send 10 minutes after the person subscribes
         * timeValue = 0, timeType = minutes means that your Triggered Campaign will send immediately after the person subscribes
         * timeValue = 1, timeType = months means that your Triggered Campaign will send 1 month after the person subscribes
         * 
         * Advanced Conditions can help control who will receive your Triggered Campaign, some example uses are limiting your Triggered campaign to users:
         *  - with a specific domain name in their email address
         *  - who sign up in a specific state
         *  - many more (See the AdvancedCondition API calls)
         * 
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>listId The ID of the list that subscriptions to will trigger</param>
         * <param>stringtimeType The type of time interval for your Triggered Campaign - timeValue and go together to define the timing rule for your Triggered Campaign. Valid values: minutes, hours, days, weeks, months</param>
         * <param>timeValue A number between 0 and 60 inclusive - and timeType go together to define the timing rule for your Triggered Campaign</param>
         * <param>stringcampaignName The name of this Campaign - not shown to recipients</param>
         * <param>stringsubject The line of the Campaign</param>
         * <param>stringsenderEmail The from email address of the Campaign</param>
         * <param>stringsenderName The from name of the Campaign</param>
         * <param>Dictionary<string, string>content A struct which specifies the of the Campaign email - can include html and text</param>
         * <param>List<object>advancedConditions An array of AdvancedCondition items that govern automation behavior</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object campaignId The ID of your new Campaign</returns>
         */
        public object Campaign_Create_Triggered_On_List_Subscription(int listId, string timeType, int timeValue, string campaignName, string subject, string senderEmail, string senderName, Dictionary<string, string> content, List<object> advancedConditions, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("listId", listId);
            args.Add("timeType", timeType);
            args.Add("timeValue", timeValue);
            args.Add("campaignName", campaignName);
            args.Add("subject", subject);
            args.Add("senderEmail", senderEmail);
            args.Add("senderName", senderName);
            args.Add("content", content);
            args.Add("advancedConditions", advancedConditions);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Create_Triggered_On_List_Subscription", args);
        }


        /**
         * <summary>Create an Triggered campaign that sends relative to a Date CustomField</summary>
         *
         * Ex: Send 10 days before the value of CustomField birthday
         * 
         * Note:  Triggered campaigns won't send until they are activated
         * 
         * timeType, timeValue, timeDirection and useCurrentYear work together to define when your Triggered Campaign fires, ex:
         * timeValue = 4, timeType = days, timeDirection = before means that your Triggered Campaign will send 4 days before the value of the given CustomField
         * timeValue = 1, timeType = months, timeDirection = after means that your Triggered Campaign will send 1 month after the value of the given CustomField
         * 
         * useCurrentYear can be used to ignore the Year value of the Date CustomField, set useCurrentYear to cause the Campaign to trigger each year on the Date CustomField's Day and Month
         * 
         * Advanced Conditions can help control who will receive your Triggered Campaign, some example uses are limiting your Triggered campaign to users:
         *  - with a specific domain name in their email address
         *  - who sign up in a specific state
         *  - many more (See the AdvancedCondition API calls)
         * 	 
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>dateCustomFieldId The ID of the CustomField of type Date whose value you want to use as a trigger</param>
         * <param>stringtimeType The type of time interval for your Triggered Campaign - timeValue and go together to define the timing rule for your Triggered Campaign. Valid values: days, weeks, months</param>
         * <param>timeValue A number between 0 and 60 inclusive - and timeType go together to define the timing rule for your Triggered Campaign</param>
         * <param>stringtimeDirection The direction of the time interval for your Triggered Campaign - timeValue and timeType go together to define the timing rule for your Triggered Campaign. Valid values: before, after</param>
         * <param>booluseCurrentYear Set to true to ignore the Year value in the specified Date CustomField and base the trigger off of the current year instead.</param>
         * <param>sendTime What hour of the day in the campaign should send in 24-hour format, give a number between 0 and 23 inclusive</param>
         * <param>stringcampaignName The name of this Campaign - not shown to recipients</param>
         * <param>stringsubject The line of the Campaign</param>
         * <param>stringsenderEmail The from email address of the Campaign</param>
         * <param>stringsenderName The from name of the Campaign</param>
         * <param>Dictionary<string, string>content A struct which specifies the of the Campaign email - can include html and text</param>
         * <param>List<object>advancedConditions An array of AdvancedCondition items that govern automation behavior</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object campaignId The ID of your new Campaign</returns>
         */
        public object Campaign_Create_Triggered_On_Date_CustomField(int dateCustomFieldId, string timeType, int timeValue, string timeDirection, bool useCurrentYear, int sendTime, string campaignName, string subject, string senderEmail, string senderName, Dictionary<string, string> content, List<object> advancedConditions, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("dateCustomFieldId", dateCustomFieldId);
            args.Add("timeType", timeType);
            args.Add("timeValue", timeValue);
            args.Add("timeDirection", timeDirection);
            args.Add("useCurrentYear", useCurrentYear);
            args.Add("sendTime", sendTime);
            args.Add("campaignName", campaignName);
            args.Add("subject", subject);
            args.Add("senderEmail", senderEmail);
            args.Add("senderName", senderName);
            args.Add("content", content);
            args.Add("advancedConditions", advancedConditions);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Create_Triggered_On_Date_CustomField", args);
        }


        /**
         * <summary>Create a Recurring Campaign that sends regularly on a defined schedule</summary>
         *
         * <structnotes timeFrame>
         * timeFrame should be a struct containing:
         *  - string type - the type of timeFrame, valid values: daily, weekly, monthly.  Each has different required fields
         *    daily: no additional fields required
         *    weekly:
         * 		- array days An array of days that that the Recurring Campaign should send on, valid values are: sun, mon, tues, wed, thurs, fri, sat OR sunday, monday, tuesday, wednesday, thursday, friday, saturday
         *    monthly:
         * 		- int dayOfMonth What day of the month you want the Recurring Campaign to send on (1-31)
         * 
         * timeFrame examples:
         * {'type':'daily'}
         * {'type':'weekly','days':['tues']}
         * {'type':'weekly','days':['sunday','thursday']}
         * {'type':'monthly','dayOfMonth':15}
         * </structnotes timeFrame>
         * 
         * 
         * <structnotes recipients>
         * Any combination of the above can be provided in recipients.
         * Ex. 1:{'recipients':{'list':3,group:[1,2,3],'search':[2,3]}
         * Ex. 2:{'recipients':{'list':[1,2],group:1,'search':[1,2]}
         * Ex. 3:{'recipients':{'list':[1,2],group:[2,3],'search':1}
         * Ex. 4:{'recipients':{'list':1}
         * </structnotes recipients>
         * 
         * <structnotes content>
         * At least one of html or text must be provided.  If both are provided, the message will be sent as a multipart message
         * </structnotes content>
         * 
         * 
         * 
         * <structnotes exclusions>
         * Any combination of the above can be provided in exclusions.
         * Ex. 1:{'exclusions':{'list':3,group:[1,2,3],'search':[2,3]}
         * Ex. 2:{'exclusions':{'list':[1,2],group:1,'search':[1,2]}
         * Ex. 3:{'exclusions':{'list':[1,2],group:[2,3],'search':1}
         * Ex. 4:{'exclusions':{'list':1}
         * </structnotes exclusions>
         * 
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>Dictionary<string, string>timeFrame A definition of how often the Recurring Campaign should send (see notes above)</param>
         * <param>sendHour What hour of the day the Recurring Campaign should be sent, in 24 hour format (0-23)</param>
         * <param>sendMinute What minute of the day the Recurring Campaign should be sent (0-59)</param>
         * <param>stringsendTimezone What Timezone is intended for use with sendHour and sendMinute</param>
         * <param>Dictionary<string, object>recipients A struct which specifies the for your Campaign - can include list, group and search</param>
         * <param>stringcampaignName The name of this Campaign - not shown to recipients</param>
         * <param>stringsubject The line of the Campaign</param>
         * <param>stringsenderEmail The from email address of the Campaign</param>
         * <param>stringsenderName The from name of the Campaign</param>
         * <param>Dictionary<string, string>content A struct which specifies the of the Campaign email - can include html and text</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object campaignId The ID for your new Campaign</returns>
         */
        public object Campaign_Create_Recurring(Dictionary<string, string> timeFrame, int sendHour, int sendMinute, string sendTimezone, Dictionary<string, object> recipients, string campaignName, string subject, string senderEmail, string senderName, Dictionary<string, string> content, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("timeFrame", timeFrame);
            args.Add("sendHour", sendHour);
            args.Add("sendMinute", sendMinute);
            args.Add("sendTimezone", sendTimezone);
            args.Add("recipients", recipients);
            args.Add("campaignName", campaignName);
            args.Add("subject", subject);
            args.Add("senderEmail", senderEmail);
            args.Add("senderName", senderName);
            args.Add("content", content);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Create_Recurring", args);
        }


        /**
         * <summary>Create a Recurring Campaign that sends regularly on a defined schedule</summary>
         *
         * <structnotes timeFrame>
         * timeFrame should be a struct containing:
         *  - string type - the type of timeFrame, valid values: daily, weekly, monthly.  Each has different required fields
         *    daily: no additional fields required
         *    weekly:
         * 		- array days An array of days that that the Recurring Campaign should send on, valid values are: sun, mon, tues, wed, thurs, fri, sat OR sunday, monday, tuesday, wednesday, thursday, friday, saturday
         *    monthly:
         * 		- int dayOfMonth What day of the month you want the Recurring Campaign to send on (1-31)
         * 
         * timeFrame examples:
         * {'type':'daily'}
         * {'type':'weekly','days':['tues']}
         * {'type':'weekly','days':['sunday','thursday']}
         * {'type':'monthly','dayOfMonth':15}
         * </structnotes timeFrame>
         * 
         * 
         * <structnotes recipients>
         * Any combination of the above can be provided in recipients.
         * Ex. 1:{'recipients':{'list':3,group:[1,2,3],'search':[2,3]}
         * Ex. 2:{'recipients':{'list':[1,2],group:1,'search':[1,2]}
         * Ex. 3:{'recipients':{'list':[1,2],group:[2,3],'search':1}
         * Ex. 4:{'recipients':{'list':1}
         * </structnotes recipients>
         * 
         * 
         * <structnotes content>
         * At least one of htmlUrl or textUrl must be provided.  If both are provided, the message will be sent as a multipart message
         * </structnotes content>
         * 
         * 
         * <structnotes exclusions>
         * Any combination of the above can be provided in exclusions.
         * Ex. 1:{'exclusions':{'list':3,group:[1,2,3],'search':[2,3]}
         * Ex. 2:{'exclusions':{'list':[1,2],group:1,'search':[1,2]}
         * Ex. 3:{'exclusions':{'list':[1,2],group:[2,3],'search':1}
         * Ex. 4:{'exclusions':{'list':1}
         * </structnotes exclusions>
         * 
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>Dictionary<string, string>timeFrame A definition of how often the Recurring Campaign should send (see notes above)</param>
         * <param>sendHour What hour of the day the Recurring Campaign should be sent, in 24 hour format (0-23)</param>
         * <param>sendMinute What minute of the day the Recurring Campaign should be sent (0-59)</param>
         * <param>stringsendTimezone What Timezone is intended for use with sendHour and sendMinute</param>
         * <param>Dictionary<string, object>recipients A struct which specifies the for your Campaign - can include list, group and search</param>
         * <param>stringcampaignName The name of this Campaign - not shown to recipients</param>
         * <param>stringsubject The line of the Campaign</param>
         * <param>stringsenderEmail The from email address of the Campaign</param>
         * <param>stringsenderName The from name of the Campaign</param>
         * <param>Dictionary<string, string>content A struct which specifies the of the Campaign email - can include html and text</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object campaignId The ID for your new Campaign</returns>
         */
        public object Campaign_Create_Recurring_From_Url(Dictionary<string, string> timeFrame, int sendHour, int sendMinute, string sendTimezone, Dictionary<string, object> recipients, string campaignName, string subject, string senderEmail, string senderName, Dictionary<string, string> content, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("timeFrame", timeFrame);
            args.Add("sendHour", sendHour);
            args.Add("sendMinute", sendMinute);
            args.Add("sendTimezone", sendTimezone);
            args.Add("recipients", recipients);
            args.Add("campaignName", campaignName);
            args.Add("subject", subject);
            args.Add("senderEmail", senderEmail);
            args.Add("senderName", senderName);
            args.Add("content", content);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Create_Recurring_From_Url", args);
        }


        /**
         * <summary>Create a transactional mail2 campaign</summary>
         *
         * A transactional campaign is sent to one recipeient at a time for a specific purpose with
         * replacement values for tokens specific to that message.  Ex: sending a user's authorization
         * code following a signup action on your site.  Replacements will override the value of a
         * custom field with the same name in the Campaign
         * 
         * If the content of your email included a string {activation_code}, your replacements could contain {'activation_code':'123'},
         * and that value would be used only for this instance of the transactional campaign
         * 
         * <structnotes contacts>
         * Contacts is a struct which can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * A contact must have an email
         * </structnotes contacts>
         * 
         * <structnotes content>
         * At least one of html or text must be provided.  If both are provided, the message will be sent as a multipart message
         * </structnotes content>
         * 
         * 
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>Dictionary<string, string>testContact An initial contact to receive a test copy of the transactional email</param>
         * <param>Dictionary<string, string>testReplacements An initial set of test replacement values to be used for the testEmail</param>
         * <param>stringcampaignName The name of this Campaign - not shown to recipients</param>
         * <param>stringsubject The line of the Campaign</param>
         * <param>stringsenderEmail The from email address of the Campaign</param>
         * <param>stringsenderName The from name of the Campaign</param>
         * <param>Dictionary<string, string>content A struct which specifies the of the Campaign email - can include html and text</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object campaignId The ID for your new Campaign</returns>
         */
        public object Campaign_Create_Transactional(Dictionary<string, string> testContact, Dictionary<string, string> testReplacements, string campaignName, string subject, string senderEmail, string senderName, Dictionary<string, string> content, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("testContact", testContact);
            args.Add("testReplacements", testReplacements);
            args.Add("campaignName", campaignName);
            args.Add("subject", subject);
            args.Add("senderEmail", senderEmail);
            args.Add("senderName", senderName);
            args.Add("content", content);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Create_Transactional", args);
        }


        /**
         * <summary>Update properties of an existing Campaign. Only unsent campaigns in Draft status can be updated.</summary>
         *
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>campaignId The Campaign id. The campaign must be in draft mode - an exception will be thrown if not.</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing standard Info for the campaign</returns>
         */
        public object Campaign_Update_Standard(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Update_Standard", args);
        }


        /**
         * <summary>Update properties of an existing Triggered On List Subscription Campaign.  Only unactivated campaigns in Draft status can be Updated</summary>
         *
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>campaignId The Campaign id. The campaign must be in draft mode - an exception will be thrown if not.</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing standard Info for the campaign</returns>
         */
        public object Campaign_Update_Triggered_On_List_Subscription(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Update_Triggered_On_List_Subscription", args);
        }


        /**
         * <summary>Update properties of an existing Triggered On Date CustomField Campaign.  Only unactivated campaigns in Draft status can be Updated</summary>
         *
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>campaignId The Campaign id. The campaign must be in draft mode - an exception will be thrown if not.</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing standard Info for the campaign</returns>
         */
        public object Campaign_Update_Triggered_On_Date_CustomField(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Update_Triggered_On_Date_CustomField", args);
        }


        /**
         * <summary>Update properties of an existing Recurring Campaign.  Only unactivated campaigns in Draft status can be Updated</summary>
         *
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>campaignId The Campaign id. The campaign must be in draft mode - an exception will be thrown if not.</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing standard Info for the campaign</returns>
         */
        public object Campaign_Update_Recurring(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Update_Recurring", args);
        }


        /**
         * <summary>Refresh the content of a URL based Campaign in Draft status</summary>
         *
         * 
         * <structnotes links>
         *     - string url The actual URL of the link
         *     - string linktext The text of the link
         * </structnotes links>
         *
         * <param>campaignId The Campaign id. The campaign must be in draft mode - an exception will be thrown if not.</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing a preview of the campaign</returns>
         */
        public object Campaign_Refresh_Url_Content(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Refresh_Url_Content", args);
        }


        /**
         * <summary>Get Info for a specified Campaign</summary>
         *
         * 
         *
         * <param>campaignId The Campaign ID of the Campaign you want Info on</param>
         * <returns>object </returns>
         */
        public object Campaign_Get_Info(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Get_Info", args);
        }


        /**
         * <summary>Find Info for a set of Campaigns</summary>
         *
         * If no searchParameters are defined, all Campaigns will be returned
         * 
         * Note: Campaign_Find will not return more than 1000 Campaigns - if you run a Campaign_Find that would return more than 1000 Campaigns, it will throw an error
         *
         * <param>Dictionary<string, string>searchParameters A struct of search parameters, any combination of which can be used</param>
         * <returns>object </returns>
         */
        public object Campaign_Find(Dictionary<string, string> searchParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("searchParameters", searchParameters);

            return transport.MakeCall("Campaign_Find", args);
        }


        /**
         * <summary>Find count for a set of Campaigns</summary>
         *
         * If no searchParameters are defined, all Campaigns will be counted
         *
         * <param>Dictionary<string, string>searchParameters A struct of search parameters, any combination of which can be used</param>
         * <returns>object Number of campaigns</returns>
         */
        public object Campaign_Get_Count(Dictionary<string, string> searchParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("searchParameters", searchParameters);

            return transport.MakeCall("Campaign_Get_Count", args);
        }


        /**
         * <summary>Get a preview of a Campaign</summary>
         *
         * <structnotes links>
         *     - string url The actual URL of the link
         *     - string linktext The text of the link
         * </structnotes links>
         *
         * <param>campaignId The campaign you wish to preview</param>
         * <returns>object Returns a struct containing a preview of the campaign</returns>
         */
        public object Campaign_Preview(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Preview", args);
        }


        /**
         * <summary>Create a duplicate of a campaign</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to copy</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns Info about the newly created campaign</returns>
         */
        public object Campaign_Copy(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Copy", args);
        }


        /**
         * <summary>Send a test of your Campaign</summary>
         *
         * 
         * At least one of testEmail or testListId must be provided
         *
         * <param>campaignId The campaign you wish to test</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object True on success</returns>
         */
        public object Campaign_Send_Test(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Send_Test", args);
        }


        /**
         * <summary>Get a report on a Completed Campaign</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to generate a report for</param>
         * <returns>object Returns report data from the specified campaign</returns>
         */
        public object Campaign_Report(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Report", args);
        }


        /**
         * <summary>Delete a specified Campaign</summary>
         *
         * WARNING:  Unlike the webapp, this call WILL NOT prompt for confirmation
         *
         * <param>campaignId The campaign you wish to delete</param>
         * <returns>object Returns true on success</returns>
         */
        public object Campaign_Delete(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Delete", args);
        }


        /**
         * <summary>Adds an adhoc grouping of contacts to a campaign
    The recipients are added and will be sent in the next send cycle. This can be used to send transactional or ongoing type messages. In this</summary>
         *
         * case, recipients will be appended to the queue - meaning you can send the same contact the same message multiple times.
         * 
         * There is a limit of 1000 contacts per call, however you can use a loop to add more than 1000 contacts.
         * 
         * <structnotes contacts>
         * contacts is an array of structs, where each struct can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * Each contact must have a valid email
         * 
         * Ex:
         * contacts = [
         * {'email':'test@mail2.com', 'first_name':'Test', 'last_name':'User'},
         * {'email':'support@mail2.com', 1:'Support', 2:'User'}
         * ];
         * </structnotes contacts>
         *
         * <param>intcampaignId a valid campaign id</param>
         * <param>List<Dictionary<string, string> >contacts an array of contact items</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns contact add results</returns>
         */
        public object Campaign_Add_Recipients(int campaignId, List<Dictionary<string, string>> contacts, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("contacts", contacts);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Add_Recipients", args);
        }


        /**
         * <summary>Send a Campaign currently in Draft status</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to send</param>
         * <returns>object Returns boolean success and an array of issues</returns>
         */
        public object Campaign_Send(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Send", args);
        }


        /**
         * <summary>Send a new message in a transactional Campaign</summary>
         *
         * A transactional campaign is sent to one recipient at a time for a specific purpose with
         * replacement values for tokens specific to that message.  Ex: sending a user's authorization
         * code following a signup action on your site.  Replacements will override the value of a
         * custom field with the same name in the Campaign
         * 
         * If the content of your email included a string {activation_code}, your replacements could contain {'activation_code':'123'},
         * and that value would be used only for this instance of the transactional campaign
         * 
         * <structnotes contact>
         * contact is a struct, which can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * A contact must have a valid email
         * 
         * Ex:
         * contact = {'email':'test@mail2.com', 'first_name':'Test', 'last_name':'User'}
         * contact = {'email':'support@mail2.com', 1:'Support', 2:'User'}
         * </structnotes contact>
         *
         * <param>campaignId The campaign you wish to send</param>
         * <param>Dictionary<string, string>contact The you wish to send this transactional campaign to</param>
         * <param>stringsource A short description of the of your contact</param>
         * <param>Dictionary<string, string>replacements Token replacement values to be swapped in the message body</param>
         * <returns>object Returns true on success, returns a struct on failure</returns>
         */
        public object Campaign_Send_Transactional(int campaignId, Dictionary<string, string> contact, string source, Dictionary<string, string> replacements)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("contact", contact);
            args.Add("source", source);
            args.Add("replacements", replacements);

            return transport.MakeCall("Campaign_Send_Transactional", args);
        }


        /**
         * <summary>Send a new message in a transactional Campaign</summary>
         *
         * A transactional campaign is sent to one recipient at a time for a specific purpose with
         * replacement values for tokens specific to that message.  Ex: sending a user's authorization
         * code following a signup action on your site.  Replacements will override the value of a
         * custom field with the same name in the Campaign
         * 
         * If the content of your email included a string {activation_code}, your replacements could contain {'activation_code':'123'},
         * and that value would be used only for this instance of the transactional campaign
         * 
         * This function allows you to send transactional messages in batches of up to 1000 contacts, providing a replacement struct for each contact
         * 
         * If this call fails and returns an error struct, no messages will be sent.  The List_Evaluate_Contacts call can help you find potential problems
         * in your list prior to using this call.  This behavior can be bypassed with the continueOnError optionalParameter.
         * 
         * If continueOnError is specified, the number actually sent will be in the "successes" key of the return struct
         * 
         * <structnotes contacts>
         * Each contact is a struct, which can contain:
         *  - email => value
         *  - customFieldToken => value
         *  - customFieldId => value
         * 
         * A contact must have a valid email
         * 
         * Ex:
         * contact = {'email':'test@mail2.com', 'first_name': 'Test', 'last_name':'User'}
         * contact = {'email':'support@mail2.com', 1:'Support', 2:'User'}
         * </structnotes contacts>
         *
         * <param>campaignId The campaign you wish to send</param>
         * <param>List<Dictionary<string, string> >contacts An array of structs containing the you wish to send this transactional campaign to</param>
         * <param>stringsource A short description of the of your contact</param>
         * <param>List<object>replacements An array of token replacement values to be swapped in the message body</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns true on success, returns a struct on failure</returns>
         */
        public object Campaign_Send_Transactional_Multiple(int campaignId, List<Dictionary<string, string>> contacts, string source, List<object> replacements, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("contacts", contacts);
            args.Add("source", source);
            args.Add("replacements", replacements);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Send_Transactional_Multiple", args);
        }


        /**
         * <summary>Send a new message in a reconfirmation campaign</summary>
         *
         * A reconfirmation campaign is sent to one recipient at a time to request confirmation
         * that the recipient wishes to receive future campaigns.
         * 
         * This API call is disabled by default. To enable it, please contact our support representatives.
         *
         * <param>stringemail The you wish to send this transactional campaign to</param>
         * <returns>object Returns true on success, returns a struct on failure</returns>
         */
        public object Campaign_Send_Reconfirmation(string email)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("email", email);

            return transport.MakeCall("Campaign_Send_Reconfirmation", args);
        }


        /**
         * <summary>Schedule a Campaign to send at a specified date and time</summary>
         *
         * <structnotes time>
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * 'now'
         *    * 'today'
         *    * 'tomorrow'
         *    * 'first day of January 2010' (example)
         *    * 'last day of March 2010' (example)
         *    * 'Monday this week' (example)
         *    * 'Tuesday next week' (example)
         * 
         * The timeString must specify a time in the future.  No time travelling to send emails is allowed
         * </structnotes time>
         *
         * <param>campaignId The campaign you wish to schedule</param>
         * <param>stringtime a timeString in UTC timezone</param>
         * <returns>object Returns boolean success and an array of issues</returns>
         */
        public object Campaign_Schedule(int campaignId, string time)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("time", time);

            return transport.MakeCall("Campaign_Schedule", args);
        }


        /**
         * <summary>Clear a Scheduled Campaign's scheduled time and return the Campaign to Draft mode</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to schedule</param>
         * <returns>object </returns>
         */
        public object Campaign_Schedule_Cancel(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Schedule_Cancel", args);
        }


        /**
         * <summary>Resend the Campaign to Contacts who soft-bounced</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to schedule</param>
         * <returns>object Returns true on success</returns>
         */
        public object Campaign_Resend_Bounces(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Resend_Bounces", args);
        }


        /**
         * <summary>Get a list of all Contacts who have the Delivered status for the specified Campaign</summary>
         *
         * 
         * A timeString can be any of:
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * "now"
         *    * "today"
         *    * "tomorrow"
         *    * "first day of January 2010" (example)
         *    * "last day of March 2010" (example)
         *    * "Monday this week" (example)
         *    * "Tuesday next week" (example)
         *
         * <param>campaignId The campaign you wish to find delivered contacts for</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing all found contacts</returns>
         */
        public object Campaign_Get_Delivered_Contacts(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Get_Delivered_Contacts", args);
        }


        /**
         * <summary>Get a list of all Contacts who Hard Bounced for the specified Campaign</summary>
         *
         * 
         * A timeString can be any of:
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * "now"
         *    * "today"
         *    * "tomorrow"
         *    * "first day of January 2010" (example)
         *    * "last day of March 2010" (example)
         *    * "Monday this week" (example)
         *    * "Tuesday next week" (example)
         *
         * <param>campaignId The campaign you wish to find hard bounced contacts for</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing all found contacts</returns>
         */
        public object Campaign_Get_Hard_Bounced_Contacts(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Get_Hard_Bounced_Contacts", args);
        }


        /**
         * <summary>Get a list of all Contacts who Opened the specified Campaign</summary>
         *
         * Only works if the Campaign had trackOpens set to true
         * 
         * 
         * A timeString can be any of:
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * "now"
         *    * "today"
         *    * "tomorrow"
         *    * "first day of January 2010" (example)
         *    * "last day of March 2010" (example)
         *    * "Monday this week" (example)
         *    * "Tuesday next week" (example)
         *
         * <param>campaignId The campaign you wish to find contacts who opened said campaign</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing all found contacts</returns>
         */
        public object Campaign_Get_Opened_Contacts(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Get_Opened_Contacts", args);
        }


        /**
         * <summary>Get a list of all Contacts who Clicked Thru for the specified Campaign</summary>
         *
         * Only works for Campaigns who have trackClickThruHTML and/or trackClickThruText set to true
         * 
         * 
         * A timeString can be any of:
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * "now"
         *    * "today"
         *    * "tomorrow"
         *    * "first day of January 2010" (example)
         *    * "last day of March 2010" (example)
         *    * "Monday this week" (example)
         *    * "Tuesday next week" (example)
         *
         * <param>campaignId The campaign you wish to find clicked thru contacts for</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing all found contacts</returns>
         */
        public object Campaign_Get_ClickThru_Contacts(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Get_ClickThru_Contacts", args);
        }


        /**
         * <summary>Get a list of all Contacts who Replied To the specified Campaign</summary>
         *
         * Only works if the Campaign had trackReplies set to true
         * 
         * 
         * A timeString can be any of:
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * "now"
         *    * "today"
         *    * "tomorrow"
         *    * "first day of January 2010" (example)
         *    * "last day of March 2010" (example)
         *    * "Monday this week" (example)
         *    * "Tuesday next week" (example)
         *
         * <param>campaignId The campaign you wish to find contacts who replied to said campaign</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing all found contacts</returns>
         */
        public object Campaign_Get_Replied_Contacts(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Get_Replied_Contacts", args);
        }


        /**
         * <summary>Get a list of all Contacts who Unsubscribed from the specified Campaign</summary>
         *
         * 
         * A timeString can be any of:
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * "now"
         *    * "today"
         *    * "tomorrow"
         *    * "first day of January 2010" (example)
         *    * "last day of March 2010" (example)
         *    * "Monday this week" (example)
         *    * "Tuesday next week" (example)
         *
         * <param>campaignId The campaign you wish to find contacts who unsubscribed from said campaign</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing all found contacts</returns>
         */
        public object Campaign_Get_Unsubscribed_Contacts(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Get_Unsubscribed_Contacts", args);
        }


        /**
         * <summary>Get a list of all Contacts who Subscribed from the specified Campaign</summary>
         *
         * Only works if the Campaign had trackOpens set to true
         * 
         * 
         * A timeString can be any of:
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * "now"
         *    * "today"
         *    * "tomorrow"
         *    * "first day of January 2010" (example)
         *    * "last day of March 2010" (example)
         *    * "Monday this week" (example)
         *    * "Tuesday next week" (example)
         *
         * <param>campaignId The campaign you wish to find contacts who subscribed from said campaign</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing all found contacts</returns>
         */
        public object Campaign_Get_Subscribed_Contacts(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Get_Subscribed_Contacts", args);
        }


        /**
         * <summary>Get a list of all Contacts who Forwarded the specified Campaign</summary>
         *
         * 
         * A timeString can be any of:
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * "now"
         *    * "today"
         *    * "tomorrow"
         *    * "first day of January 2010" (example)
         *    * "last day of March 2010" (example)
         *    * "Monday this week" (example)
         *    * "Tuesday next week" (example)
         *
         * <param>campaignId The campaign you wish to find contacts who forwarded said campaign</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct containing all found contacts</returns>
         */
        public object Campaign_Get_Forwarded_Contacts(int campaignId, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Get_Forwarded_Contacts", args);
        }


        /**
         * <summary>Get a list of URLs in a given campaign, for use with the Campaign_Get_ClickThru_Contacts call</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to find contacts who forwarded said campaign</param>
         * <returns>object </returns>
         */
        public object Campaign_Get_Urls(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Get_Urls", args);
        }


        /**
         * <summary>Activate an Triggered Campaign</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to activate</param>
         * <returns>object </returns>
         */
        public object Campaign_Activate_Triggered(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Activate_Triggered", args);
        }


        /**
         * <summary>Activate a Recurring Campaign</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to activate</param>
         * <returns>object </returns>
         */
        public object Campaign_Activate_Recurring(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Activate_Recurring", args);
        }


        /**
         * <summary>Deactivate an active Triggered campaign</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to deactivate</param>
         * <returns>object Returns true on success</returns>
         */
        public object Campaign_Deactivate_Triggered(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Deactivate_Triggered", args);
        }


        /**
         * <summary>Deactivate an active recurring campaign</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to deactivate</param>
         * <returns>object Returns true on success</returns>
         */
        public object Campaign_Deactivate_Recurring(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Deactivate_Recurring", args);
        }


        /**
         * <summary>Create a multivariate split mail2 campaign - used to help determine the most effective configuration for your campaigns.</summary>
         *
         * An MV Split campaign is a more advanced version of the commonly-used A/B Split testing strategy.  An A/B Split test can be done using the Split campaign type - but with MV Splits, you aren't limited to just two testing scenarios.
         * 
         * Campaign_Create_Split works almost identically to Campaign_Create_Standard - the same parameters apply and the base values of the campaign become your 'A' split.  Additional splits are created using the splitParts parameter and specifying override values for the 'A' split.  It is possible to have a split be perfectly identical to the 'A' split - this can be used to test what time of day is best to send your campaign at (see: Campaign_Schedule_Split_Parts), or you can override as many or as few properties as you want.
         * 
         * Each element in the splitParts property will cause a new split to be created, and they will be labelled alphabetically, with the 'root' split being 'A'.
         * 
         * Each split will receive a percentage of your recipients as specified in the optionalParameter splitPercent.  The remainder can be sent using Campaign_Schedule_Split_Remainder or Campaign_Send_Split_Remainder.  Ex: splitPercent 5 with 3 splits = 15% of your contacts used in testing, 85% used in sending remainder
         * 
         * 
         * <structnotes recipients>
         * Any combination of the above can be provided in recipients.
         * Ex. 1:{'recipients':{'list':3,group:[1,2,3],'search':[2,3]}
         * Ex. 2:{'recipients':{'list':[1,2],group:1,'search':[1,2]}
         * Ex. 3:{'recipients':{'list':[1,2],group:[2,3],'search':1}
         * Ex. 4:{'recipients':{'list':1}
         * </structnotes recipients>
         * 
         * 
         * <structnotes content>
         * At least one of html or text must be provided.  If both are provided, the message will be sent as a multipart message
         * </structnotes content>
         * 
         * 
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>Dictionary<string, object>recipients A struct which specifies the for your Campaign - can include list, group and search</param>
         * <param>stringcampaignName The name of this Campaign - not shown to recipients</param>
         * <param>stringsubject The line of the Campaign</param>
         * <param>stringsenderEmail The from email address of the Campaign</param>
         * <param>stringsenderName The from name of the Campaign</param>
         * <param>Dictionary<string, string>content A struct which specifies the of the Campaign email - can include html and text</param>
         * <param>List<object>splitParts An array of structs containing override values for each split - each struct will cause a new split to be created</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object campaignId The ID for your new Campaign</returns>
         */
        public object Campaign_Create_Split(Dictionary<string, object> recipients, string campaignName, string subject, string senderEmail, string senderName, Dictionary<string, string> content, List<object> splitParts, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("recipients", recipients);
            args.Add("campaignName", campaignName);
            args.Add("subject", subject);
            args.Add("senderEmail", senderEmail);
            args.Add("senderName", senderName);
            args.Add("content", content);
            args.Add("splitParts", splitParts);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Create_Split", args);
        }


        /**
         * <summary>Update properties of an existing Campaign.  Only unsent campaigns in Draft status can be Updated</summary>
         *
         * <structnotes facebookAutopost>
         * You'll need to have Facebook integration configured in order to use this feature. This campaign will be also posted online. facebookAccount must be provided to use this feature. The other parameters are optional and will be automatically generated by Facebook if not provided.
         * </structnotes facebookAutopost>
         * 
         * 
         * <structnotes twitterAutopost>
         * You'll need to have Twitter integration configured in order to use this feature. This campaign will be also posted online. The post content is automatically generated from the campaign's subject.
         * </structnotes twitterAutopost>
         *
         * <param>campaignId The Campaign id. The campaign must be in draft mode - an exception will be thrown if not.</param>
         * <param>List<object>partIds An array of partIds, each element being a single letter A-Z</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object Returns a struct of structs, a split ID with</returns>
         */
        public object Campaign_Update_Split_Parts(int campaignId, List<object> partIds, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("partIds", partIds);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Update_Split_Parts", args);
        }


        /**
         * <summary>Get the alphabetic split IDs for the parts of a split campaign</summary>
         *
         * 
         *
         * <param>campaignId The Campaign ID. The campaign must be a split campaign</param>
         * <returns>object An array of split part IDs</returns>
         */
        public object Campaign_Get_Split_Part_Ids(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Get_Split_Part_Ids", args);
        }


        /**
         * <summary>Get the Campaign_Report for the split winner/remainder, sent with Campaign_Send_Split_Remainder, Campaign_Schedule_Split_Remainder or Campaign_Schedule_Split_Winner</summary>
         *
         * 
         *
         * <param>campaignId The Campaign ID. The campaign must be a split campaign</param>
         * <returns>object See Campaign_Report</returns>
         */
        public object Campaign_Get_Split_Winner_Report(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Get_Split_Winner_Report", args);
        }


        /**
         * <summary>Get information for specific splitParts for your MV Split Campaign</summary>
         *
         * 
         *
         * <param>campaignId The Campaign ID. The campaign must be a split campaign</param>
         * <param>List<object>partIds An array of partIds, each element being a single letter A-Z</param>
         * <returns>object Report info</returns>
         */
        public object Campaign_Get_Split_Parts_Info(int campaignId, List<object> partIds)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("partIds", partIds);

            return transport.MakeCall("Campaign_Get_Split_Parts_Info", args);
        }


        /**
         * <summary>Schedule specified split parts to send at the given time.  Schedules all specified parts for the same time</summary>
         *
         * Note:  Once you send or schedule one part of a split campaign, no part of that split campaign can be modified
         * 
         * <structnotes time>
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * 'now'
         *    * 'today'
         *    * 'tomorrow'
         *    * 'first day of January 2010' (example)
         *    * 'last day of March 2010' (example)
         *    * 'Monday this week' (example)
         *    * 'Tuesday next week' (example)
         * 
         * The timeString must specify a time in the future.  No time travelling to send emails is allowed
         * </structnotes time>
         *
         * <param>campaignId The Campaign ID. The campaign must be a split campaign in draft mode</param>
         * <param>List<object>partIds An array of alphabetic split part IDs</param>
         * <param>stringtime a timeString in UTC timezone</param>
         * <returns>object Returns boolean success and an array of issues</returns>
         */
        public object Campaign_Schedule_Split_Parts(int campaignId, List<object> partIds, string time)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("partIds", partIds);
            args.Add("time", time);

            return transport.MakeCall("Campaign_Schedule_Split_Parts", args);
        }


        /**
         * <summary>Clear a Scheduled Campaign's scheduled time and return the Campaign to Draft mode</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to schedule</param>
         * <param>List<object>partIds An array of alphabetic split part IDs</param>
         * <returns>object True on success</returns>
         */
        public object Campaign_Schedule_Cancel_Split_Parts(int campaignId, List<object> partIds)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("partIds", partIds);

            return transport.MakeCall("Campaign_Schedule_Cancel_Split_Parts", args);
        }


        /**
         * <summary>Send specified split parts immediately</summary>
         *
         * Note:  Once you send or schedule one part of a split campaign, no part of that split campaign can be modified
         *
         * <param>campaignId The Campaign ID. The campaign must be a split campaign in draft mode</param>
         * <param>List<object>partIds An array of alphabetic split part IDs</param>
         * <returns>object Returns boolean success and an array of issues</returns>
         */
        public object Campaign_Send_Split_Parts(int campaignId, List<object> partIds)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("partIds", partIds);

            return transport.MakeCall("Campaign_Send_Split_Parts", args);
        }


        /**
         * <summary>Send a test message for each of the specified split partIds</summary>
         *
         * 
         * At least one of testEmail or testListId must be provided
         *
         * <param>campaignId The campaign you wish to test</param>
         * <param>List<object>partIds An array of alphabetic split part IDs</param>
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object True on success</returns>
         */
        public object Campaign_Send_Split_Test(int campaignId, List<object> partIds, Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("partIds", partIds);
            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Campaign_Send_Split_Test", args);
        }


        /**
         * <summary>Schedule the 'winning' split manually - the part specified will receive the remaining unsent recipients that were not allocated to splits</summary>
         *
         * Campaign_Get_Split_Comparison has data to help you decide which split performed has performed the best so far
         * 
         * <structnotes time>
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * 'now'
         *    * 'today'
         *    * 'tomorrow'
         *    * 'first day of January 2010' (example)
         *    * 'last day of March 2010' (example)
         *    * 'Monday this week' (example)
         *    * 'Tuesday next week' (example)
         * 
         * The timeString must specify a time in the future.  No time travelling to send emails is allowed
         * </structnotes time>
         *
         * <param>campaignId The split campaign for which you wish to schedule the remainder</param>
         * <param>stringpartId The of the split part you want the declare as the 'winner' and allocate remaining recipients to</param>
         * <param>stringtime a timeString in UTC timezone</param>
         * <returns>object Returns boolean success and an array of issues</returns>
         */
        public object Campaign_Schedule_Split_Remainder(int campaignId, string partId, string time)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("partId", partId);
            args.Add("time", time);

            return transport.MakeCall("Campaign_Schedule_Split_Remainder", args);
        }


        /**
         * <summary>Clear a Scheduled Split Remainder's scheduled time</summary>
         *
         * 
         *
         * <param>campaignId The campaign you wish to schedule</param>
         * <returns>object True on success</returns>
         */
        public object Campaign_Schedule_Cancel_Split_Remainder(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Schedule_Cancel_Split_Remainder", args);
        }


        /**
         * <summary>Send the 'winning' split manually - the part specified will receive the remaining unsent recipients that were not allocated to splits</summary>
         *
         * 
         *
         * <param>campaignId The split campaign for which you wish to send the remainder</param>
         * <param>stringpartId The of the split part you want the declare as the 'winner' and allocate remaining recipients to</param>
         * <returns>object Returns boolean success and an array of issues</returns>
         */
        public object Campaign_Send_Split_Remainder(int campaignId, string partId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("partId", partId);

            return transport.MakeCall("Campaign_Send_Split_Remainder", args);
        }


        /**
         * <summary>Schedule the Winner for your split campaign.  When the time comes, it will evaluate the winCriteria and send the remainder to the winning split.</summary>
         *
         * It is recommended that your split go out no sooner than 1 day after your splits go out.  If your splits have not all completed sending when the scheduled Winner triggers, no Winner will be chosen or sent.
         * 
         * You can send or schedule a split remainder after a Winner is scheduled to be determined, doing so will override the scheduled Winner.
         * 
         * <structnotes time>
         *  - A specified date/time in the format: YYYY-MM-DD
         *  - A relative time string including, but not limited to:
         *    * 'now'
         *    * 'today'
         *    * 'tomorrow'
         *    * 'first day of January 2010' (example)
         *    * 'last day of March 2010' (example)
         *    * 'Monday this week' (example)
         *    * 'Tuesday next week' (example)
         * 
         * The timeString must specify a time in the future.  No time travelling to send emails is allowed
         * </structnotes time>
         * 
         * <structnotes winCriteria>
         * The following values are valid for winCriteria:
         * - TotalOpened
         * - TotalClickedThru
         * - TotalReplied
         * - TotalForwarded
         * </structnotes winCriteria>
         *
         * <param>campaignId The Campaign ID. The campaign must be a split campaign. The splits must be complete at the time specified for a winner to be scheduled</param>
         * <param>stringtime A timeString in the UTC timezone</param>
         * <param>List<object>winCriteria An array of criteria to be evaluated in the order specified (these are the same criteria returned in Campaign_Get_Split_Comparison)</param>
         * <returns>object True on success</returns>
         */
        public object Campaign_Schedule_Split_Winner(int campaignId, string time, List<object> winCriteria)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);
            args.Add("time", time);
            args.Add("winCriteria", winCriteria);

            return transport.MakeCall("Campaign_Schedule_Split_Winner", args);
        }


        /**
         * <summary>Cancel a previously scheduled Winner evaluation</summary>
         *
         * 
         *
         * <param>campaignId The Campaign ID. The campaign must be a split campaign with a currently scheduled winner.</param>
         * <returns>object True on success</returns>
         */
        public object Campaign_Schedule_Cancel_Split_Winner(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Schedule_Cancel_Split_Winner", args);
        }


        /**
         * <summary>Get the Split Comparison report for a split campaign</summary>
         *
         * This data compares the performance of all splits across many factors:  Clickthrus, Opens, Bounces, Complaints, Forwards and more.  Returns a struct of structs, with the key being the data point being compared and the value as a struct containing the performance of each split
         * 
         * This function should not be called until your split sends are complete and your recipients have had time to read your split campaign
         *
         * <param>campaignId The campaign you wish to test</param>
         * <returns>object Returns a struct of structs, a breakdown of how each split performed against each metric - with a 'Sole Winner', 'Sole Loser', 'Tie', 'Tied Winner' or 'Tied Loser' status for each campaign on each metric.  Use this data to determine the winner of your split campaign</returns>
         */
        public object Campaign_Get_Split_Comparison(int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("campaignId", campaignId);

            return transport.MakeCall("Campaign_Get_Split_Comparison", args);
        }


        /**
         * <summary>List current HostedAttachments</summary>
         *
         * Returns a struct with the filename as the key and an array of all campaigns 
         * Example:
         * {"test.gif":[1,2,3],"test.doc":[]}
         *
         * <returns>object Returns a struct with the filename as the key and a list of all campaigns that have the HostedAttachment added as the value</returns>
         */
        public object HostedAttachment_List()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("HostedAttachment_List", args);
        }


        /**
         * <summary>Add a HostedAttachment to the server</summary>
         *
         * <structnotes attachment>
         * attachment should be the contents of the attachment, this is what will be stored in the attachment when it is written
         * 
         * Valid attachment types are:
         *  - gif
         *  - jpg
         *  - png
         *  - bmp
         *  - docx
         *  - pptx
         *  - xlsx
         *  - doc
         *  - xls
         *  - ppt
         *  - pdf
         *  - rtf
         *  - odt
         *  - ods
         *  - odp
         *  - txt
         *  - html
         *  - csv
         *  - mp3
         *  - avi
         *  - mpg
         *  - mov
         *  - wma
         *  - wmv
         *  - zip
         *  - gz
         *  - bz2
         * </structnotes attachment>
         *
         * <param>stringfilename name The and extension for your attachment</param>
         * <param>stringattachment The body of the attachment</param>
         * <returns>object Returns the URL of the HostedAttachment for use in your Campaigns</returns>
         */
        public object HostedAttachment_Add(string filename, string attachment)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("filename", filename);
            args.Add("attachment", attachment);

            return transport.MakeCall("HostedAttachment_Add", args);
        }


        /**
         * <summary>Add a HostedAttachment to a Campaign</summary>
         *
         * 
         *
         * <param>stringfilename The of your existing attachment</param>
         * <param>campaignId The ID of the Campaign in Draft Mode you want to add the attachment to</param>
         * <returns>object Returns true on success</returns>
         */
        public object HostedAttachment_Add_To_Campaign(string filename, int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("filename", filename);
            args.Add("campaignId", campaignId);

            return transport.MakeCall("HostedAttachment_Add_To_Campaign", args);
        }


        /**
         * <summary>Remove an existing HostedAttachment from a Campaign without deleting the HostedAttachment from the server</summary>
         *
         * 
         *
         * <param>stringfilename The of the existing HostedAttachment</param>
         * <param>campaignId The ID of the Campaign you want to remove the attachment from</param>
         * <returns>object Returns true on success</returns>
         */
        public object HostedAttachment_Remove_From_Campaign(string filename, int campaignId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("filename", filename);
            args.Add("campaignId", campaignId);

            return transport.MakeCall("HostedAttachment_Remove_From_Campaign", args);
        }


        /**
         * <summary>Delete a HostedAttachment from the server and remove it from all Campaigns</summary>
         *
         * 
         *
         * <param>stringfilename The of the existing attachment</param>
         * <returns>object Returns true on success</returns>
         */
        public object HostedAttachment_Delete(string filename)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("filename", filename);

            return transport.MakeCall("HostedAttachment_Delete", args);
        }


        /**
         * <summary>Get the total number of sent emails for your account</summary>
         *
         * 
         *
         * <param>Dictionary<string, string>optionalParameters </param>
         * <returns>object </returns>
         */
        public object Account_Get_Send_Count(Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("Account_Get_Send_Count", args);
        }


        /**
         * <summary>Get the quota, used and free space for your account</summary>
         *
         * 
         *
         * <returns>object Returns a struct containing your quota, used and free space</returns>
         */
        public object Account_Get_Hosted_Content_Info()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Account_Get_Hosted_Content_Info", args);
        }


        /**
         * <summary>Retrieve the number of block sends remaining in a Block Send Quota account</summary>
         *
         * 
         *
         * <returns>object The number of sends remaining</returns>
         */
        public object Account_Get_Block_Sends_Remaining()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Account_Get_Block_Sends_Remaining", args);
        }


        /**
         * <summary>Confirm that an AdvancedCondition for use in SavedSearch or Campaign is valid</summary>
         *
         * Ex:
         * {'type':'customField','token':'first_name','condition':'contains','value':'steve'}
         * {'type':'addedOn','after':'2010-01-11','before':'2010-06-16','selectedAreas':['appAddEditContact','appSendTest']}
         * {'type':'addedOn','before':'2010-06-16'}
         * {'type':'contactStatus','condition':'isNot','value':'globallyUnsubscribed'}
         * {'type':'listStatus','condition':'unsubscribedFrom','listId':1}
         *
         * <param>Dictionary<string, string>condition A struct following a format defined in AdvancedCondition_List_Conditions</param>
         * <returns>object </returns>
         */
        public object AdvancedCondition_Check_Condition(Dictionary<string, string> condition)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("condition", condition);

            return transport.MakeCall("AdvancedCondition_Check_Condition", args);
        }


        /**
         * <summary>Returns a list of all of your available AdvancedConditions for use in
    Campaign and SavedSearch functions</summary>
         *
         * 
         *
         * <returns>object Returns a list of valid AdvancedConditions</returns>
         */
        public object AdvancedCondition_List_Conditions()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("AdvancedCondition_List_Conditions", args);
        }


        /**
         * <summary>Get Selected Area info for use with Automated Campaigns</summary>
         *
         * 
         *
         * <returns>object Returns a list of valid SelectedAreas for your account for use with Automated Campaigns</returns>
         */
        public object AdvancedCondition_Get_SelectedAreas()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("AdvancedCondition_Get_SelectedAreas", args);
        }


        /**
         * <summary>Get a mail2 Message Quality Score for an email</summary>
         *
         * 
         *
         * <param>stringfromEmail The email address your email will be sent from</param>
         * <param>stringsubject The of your email</param>
         * <param>stringhtml The HTML body of your message</param>
         * <param>stringtext The plain version of your message</param>
         * <returns>object A struct containing your score and some detailed information on any issues encountered (see notes above)</returns>
         */
        public object Util_Get_MQS(string fromEmail, string subject, string html, string text)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("fromEmail", fromEmail);
            args.Add("subject", subject);
            args.Add("html", html);
            args.Add("text", text);

            return transport.MakeCall("Util_Get_MQS", args);
        }


        /**
         * <summary>Template_List returns a struct of your templates available for use in the new Template Controller</summary>
         *
         * 
         *
         * <param>stringgetByFolder Fetch templates by folder</param>
         * <returns>object A struct where the key is the template_name and the value is the template_id (Note: This is reversed from most API calls)</returns>
         */
        public object Template_List(string getByFolder)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("getByFolder", getByFolder);

            return transport.MakeCall("Template_List", args);
        }


        /**
         * <summary>Add a new Template for use with the new Template Controller</summary>
         *
         * Please see the Template Controller documentation for instructions on making your templates configurable
         * 
         * This function is used by the "Save As" functionality in the Template Controller
         *
         * <param>stringname The of your new template</param>
         * <param>stringhtml The HTML content of your new template</param>
         * <returns>object The templateId of your new template</returns>
         */
        public object Template_Add(string name, string html)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("name", name);
            args.Add("html", html);

            return transport.MakeCall("Template_Add", args);
        }


        /**
         * <summary>Get a list of tokens available for use in the new Template Controller</summary>
         *
         * This function is used by the new Template Controller
         *
         * <returns>object Struct of tokens</returns>
         */
        public object Template_Get_Tokens()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Template_Get_Tokens", args);
        }


        /**
         * <summary>Transfer content from one template to another.  This will only work if the templates have been set up
      in advance to use the same IDs for corresponding containers.  See the Template Controller documentation</summary>
         *
         *   for reference
         * 
         * This function is used by the new Template Controller
         *
         * <param>templateId The template ID that you're moving to</param>
         * <param>stringcontent The full HTML of the old template</param>
         * <returns>object The full HTML content of the new template, with content transferred from the old template where possible</returns>
         */
        public object Template_Transfer_Content(int templateId, string content)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("templateId", templateId);
            args.Add("content", content);

            return transport.MakeCall("Template_Transfer_Content", args);
        }


        /**
         * <summary>Get the HTML of a template by ID</summary>
         *
         * This function is used by the new Template Controller
         *
         * <param>templateId The ID of the template of which you want the content</param>
         * <returns>object The full HTML content of the specified template</returns>
         */
        public object Template_Get_Content(int templateId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("templateId", templateId);

            return transport.MakeCall("Template_Get_Content", args);
        }


        /**
         * <summary>Get the content of the default footer set for your account</summary>
         *
         * 
         *
         * <returns>object A struct containing the HTML and Text parts of your default footer</returns>
         */
        public object Footer_Get_Default()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Footer_Get_Default", args);
        }


        /**
         * <summary>List all current footers</summary>
         *
         * Note:  The default footer is ID 0
         *
         * <returns>object A struct of footers, with the key being the footer ID and the value being the footer name</returns>
         */
        public object Footer_List()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Footer_List", args);
        }


        /**
         * <summary>Get the HTML and Text for a footer by ID</summary>
         *
         * 
         *
         * <param>footerId The ID of the footer for which you want the contents</param>
         * <returns>object A struct containing the HTML and Text parts of your default footer</returns>
         */
        public object Footer_Get_Contents(int footerId)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("footerId", footerId);

            return transport.MakeCall("Footer_Get_Contents", args);
        }


        /**
         * <summary>Add a new footer for use with campaigns</summary>
         *
         * 
         *
         * <param>stringname The of your new footer</param>
         * <param>stringhtml The HTML content of your new footer</param>
         * <param>stringtext The Text content of your new footer</param>
         * <returns>object The ID of your new footer</returns>
         */
        public object Footer_Add(string name, string html, string text)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("name", name);
            args.Add("html", html);
            args.Add("text", text);

            return transport.MakeCall("Footer_Add", args);
        }


        /**
         * <summary>Change the HTML and Text parts of an existing footer</summary>
         *
         * 
         *
         * <param>footerId The ID of the footer you want to change</param>
         * <param>stringhtml The new HTML content of your footer</param>
         * <param>stringtext The new Text content of your footer</param>
         * <returns>object True on success</returns>
         */
        public object Footer_Update(int footerId, string html, string text)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("footerId", footerId);
            args.Add("html", html);
            args.Add("text", text);

            return transport.MakeCall("Footer_Update", args);
        }


        /**
         * <summary>Report_Get_Bulk_Webhooks will get all of the bulk webhooks report stored being stored for you, in case you missed one of the bulk hooks.  This call only gives you the URLs of the stored reports. You must still fetch them.</summary>
         *
         * Data follows the same format as the original webhook return. By default, you will receive multiple new line delimited JSON objects for the bulk webhook return.  If you alternatively requested a single JSON payload, the stored reports will be in that format instead with a .json extension.
         *
         * <returns>object Returns an array of filenames which hold your stored bulk hooks</returns>
         */
        public object Report_Get_Bulk_Webhooks()
        {
            object data;
            Arguments args = new Arguments();


            return transport.MakeCall("Report_Get_Bulk_Webhooks", args);
        }


        /**
         * <summary>List all social connections</summary>
         *
         * 
         *
         * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
         * <returns>object A struct of social connections</returns>
         */
        public object SocialConnection_List(Dictionary<string, string> optionalParameters)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("optionalParameters", optionalParameters);

            return transport.MakeCall("SocialConnection_List", args);
        }


        /**
         * <summary>Get all possible post locations for a specific social connection</summary>
         *
         * 
         *
         * <param>id Retrieve a specific social connection</param>
         * <returns>object A struct of social connections</returns>
         */
        public object SocialConnection_Get_Locations(int id)
        {
            object data;
            Arguments args = new Arguments();

            args.Add("id", id);

            return transport.MakeCall("SocialConnection_Get_Locations", args);
        }

    }
}