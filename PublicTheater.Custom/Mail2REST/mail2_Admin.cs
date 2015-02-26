using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using PublicTheater.Custom.Mail2REST;

namespace PublicTheater.Custom.Mail2REST{
	public class mail2Reseller {
        private Transport transport;
		
        public mail2Reseller(string api_key, bool use_https) {
        	transport = new Transport(api_key, use_https, "");
    	}

		    		
    	    		
    /**
     * <summary>Create a new mail2 account with the given parameters</summary>
     *
     * 
     *
	 * <param>stringclientName Name for the account</param>
	 * <param>stringadminEmail Default email address for the account</param>
	 * <param>stringuserName User name for the new account - must be unique</param>
	 * <param>stringpassword Password for the new account</param>
	 * <param>stringhomePage URL for the account - will be included in the unsubscribe screen</param>
	 * <param>stringlogoUrl URL to the logo image - will be included in the unsubscribe screen</param>
	 * <param>stringclientAddr1 Physical address for the account - will be included in the footer of messages</param>
	 * <param>stringclientCity Physical address for the account - will be included in the footer of messages</param>
	 * <param>stringclientState Physical address for the account - will be included in the footer of messages</param>
	 * <param>stringclientZip Physical address for the account - will be included in the footer of messages</param>
	 * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
	 * <returns>object The user ID of the new account</returns>
	 */
    	public object Admin_Create_Account( string clientName, string adminEmail, string userName, string password, string homePage, string logoUrl, string clientAddr1, string clientCity, string clientState, string clientZip, Dictionary<string, string> optionalParameters ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientName", clientName);
			args.Add("adminEmail", adminEmail);
			args.Add("userName", userName);
			args.Add("password", password);
			args.Add("homePage", homePage);
			args.Add("logoUrl", logoUrl);
			args.Add("clientAddr1", clientAddr1);
			args.Add("clientCity", clientCity);
			args.Add("clientState", clientState);
			args.Add("clientZip", clientZip);
			args.Add("optionalParameters", optionalParameters);

    		return transport.MakeCall("Admin_Create_Account", args);
    	}
    	    		
    	    		
    /**
     * <summary>Create a new mail2 account using minimal initial information</summary>
     *
     * This call enables you to sign up accounts with less information (does not require address, logo or homepage), but these accounts must provide this information before sending
     *
	 * <param>stringclientName Name for the account</param>
	 * <param>stringadminEmail Default email address for the account</param>
	 * <param>stringuserName User name for the new account - must be unique</param>
	 * <param>stringpassword Password for the new account</param>
	 * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
	 * <returns>object The user ID of the new account</returns>
	 */
    	public object Admin_Create_Account_Minimal( string clientName, string adminEmail, string userName, string password, Dictionary<string, string> optionalParameters ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientName", clientName);
			args.Add("adminEmail", adminEmail);
			args.Add("userName", userName);
			args.Add("password", password);
			args.Add("optionalParameters", optionalParameters);

    		return transport.MakeCall("Admin_Create_Account_Minimal", args);
    	}
    	    		
    	    		
    /**
     * <summary>Get a list of your current clients</summary>
     *
     * 
     *
	 * <returns>object Struct of current client accounts { clientId : clientName, clientId : clientName }</returns>
	 */
    	public object Admin_Get_Accounts(  ) {
    		object data;
    		Arguments args = new Arguments();


    		return transport.MakeCall("Admin_Get_Accounts", args);
    	}
    	    		
    	    		
    /**
     * <summary>Get a list of your clients based on parameters</summary>
     *
     * 
     *
	 * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
	 * <returns>object Struct of accounts { { "clientId":"1", "clientStatus":"active", "clientCreated":"2012-01-01 00:00:00" }, { "clientId":"2", "clientStatus":"disabled", "clientCreated":"2012-02-02 02:00:00" }</returns>
	 */
    	public object Admin_Find_Accounts( Dictionary<string, string> optionalParameters ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("optionalParameters", optionalParameters);

    		return transport.MakeCall("Admin_Find_Accounts", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Get_Account_Info retrieves all the info about an Account, including the Plan information</summary>
     *
     * 
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <returns>object Returns a struct of the account's properties</returns>
	 */
    	public object Admin_Get_Account_Info( int clientId ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);

    		return transport.MakeCall("Admin_Get_Account_Info", args);
    	}
    	    		
    	    		
    /**
     * <summary>Modifies multiple properties of an existing mail2 account created by your account.  Only fields in optionalParameters will be modified</summary>
     *
     * This API call will ignore any parameters not explicitly allowed in optionalParameters.
	 * 
	 * This API call does not handle any changes that could result in a change in billing.  For quotas, limits and similar changes, please use the Admin_Change_Account_Plan API call
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
	 * <returns>object The number of fields successfully modified</returns>
	 */
    	public object Admin_Modify_Account( int clientId, Dictionary<string, string> optionalParameters ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("optionalParameters", optionalParameters);

    		return transport.MakeCall("Admin_Modify_Account", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Get_Account_Plan returns an struct of Account Plan details for one of your clients</summary>
     *
     * 
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <returns>object Returns a struct of Account Plan details</returns>
	 */
    	public object Admin_Get_Account_Plan( int clientId ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);

    		return transport.MakeCall("Admin_Get_Account_Plan", args);
    	}
    	    		
    	    		
    /**
     * <summary>Change Account Plan allows you to change everything about an Account's Plan at once</summary>
     *
     * If you only want to modify one of the properties, it may be easier to use the individual change API calls
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>hostedLimit hostedClientLimit Hosted Client Limit, valid values: 10, 20, 30, 40, 50</param>
	 * <param>maxCustomUsers maximumCustomUsers Maximum Custom Users, valid values: 3, 4, 8, 13, 28</param>
	 * <param>stringmail2Package [DEPRECATED] Package is no longer required - an empty string will work fine. Previous valid values: PACKAGE_CONTACTS_EMAIL, PACKAGE_SURVEYS, PACKAGE_FULL, PACKAGE_MIGRATE will be accepted to keep legacy code from breaking</param>
	 * <param>stringaccountType Account type, valid values: NO_EMAIL, CONTACT_QUOTA, BLOCK_SENDS, UNLIMITED</param>
	 * <param>contactQuota Contact quota, valid values: 100, 250, 500, 1000, 2500, 5000, 7500, 10000, 15000, 20000, 25000</param>
	 * <param>contractLength Contract length, valid values: 1, 12</param>
	 * <param>paymentFrequency Payment frequency, valid values: 1, 3, 6, 12</param>
	 * <param>boolpoweredBy Display Powered By mail2 logo</param>
	 * <param>boolsurveys optional Enable or disable for the account, defaults to false</param>
	 * <returns>object Returns true on success</returns>
	 */
    	public object Admin_Change_Account_Plan( int clientId, int hostedLimit, int maxCustomUsers, string mail2Package, string accountType, int contactQuota, int contractLength, int paymentFrequency, bool poweredBy, bool surveys ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("hostedLimit", hostedLimit);
			args.Add("maxCustomUsers", maxCustomUsers);
			args.Add("package", mail2Package);
			args.Add("accountType", accountType);
			args.Add("contactQuota", contactQuota);
			args.Add("contractLength", contractLength);
			args.Add("paymentFrequency", paymentFrequency);
			args.Add("poweredBy", poweredBy);
			args.Add("surveys", surveys);

    		return transport.MakeCall("Admin_Change_Account_Plan", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Change_Account_Hosted_Limit allows you to change the Hosted Limit (in Megabytes) for your clients</summary>
     *
     * Valid values: 10, 20, 30, 40, 50
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>hostedLimit Hosted Limit, valid values: 10, 20, 30, 40, 50</param>
	 * <returns>object Returns true on success</returns>
	 */
    	public object Admin_Change_Account_Hosted_Limit( int clientId, int hostedLimit ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("hostedLimit", hostedLimit);

    		return transport.MakeCall("Admin_Change_Account_Hosted_Limit", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Change_Account_Maximum_Custom_Users allows you to change the Maximum Custom Users of your clients</summary>
     *
     * Valid values: 3, 4, 8, 13, 28
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>maxCustomUsers Maximum Custom Users, valid values: 3, 4, 8, 13, 28</param>
	 * <returns>object Returns true on success</returns>
	 */
    	public object Admin_Change_Account_Maximum_Custom_Users( int clientId, int maxCustomUsers ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("maxCustomUsers", maxCustomUsers);

    		return transport.MakeCall("Admin_Change_Account_Maximum_Custom_Users", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Change_Account_Package allows you to change the Package of your clients</summary>
     *
     * Valid values: PACKAGE_CONTACTS_EMAIL, PACKAGE_SURVEYS, PACKAGE_FULL, PACKAGE_MIGRATE
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>stringmail2Package Package, valid values: PACKAGE_CONTACTS_EMAIL, PACKAGE_SURVEYS, PACKAGE_FULL, PACKAGE_MIGRATE</param>
	 * <returns>object Returns true on success</returns>
	 */
    	public object Admin_Change_Account_Package( int clientId, string mail2Package ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("package", mail2Package);

    		return transport.MakeCall("Admin_Change_Account_Package", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Change_Account_Type allows you to change the Account Type of your clients</summary>
     *
     * Valid values: NO_EMAIL, CONTACT_QUOTA, BLOCK_SENDS, UNLIMITED
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>stringaccountType Account type, valid values: NO_EMAIL, CONTACT_QUOTA, BLOCK_SENDS, UNLIMITED</param>
	 * <returns>object Returns true on success</returns>
	 */
    	public object Admin_Change_Account_Type( int clientId, string accountType ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("accountType", accountType);

    		return transport.MakeCall("Admin_Change_Account_Type", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Change_Account_Contact_Quota allows you to change the Contact Quota of your clients</summary>
     *
     * Valid values: 100, 250, 500, 1000, 2500, 5000, 7500, 10000, 15000, 20000, 25000
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>contactQuota Contact quota, valid values: 100, 250, 500, 1000, 2500, 5000, 7500, 10000, 15000, 20000, 25000</param>
	 * <returns>object True on success</returns>
	 */
    	public object Admin_Change_Account_Contact_Quota( int clientId, int contactQuota ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("contactQuota", contactQuota);

    		return transport.MakeCall("Admin_Change_Account_Contact_Quota", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Change_Account_Contract_Length allows you to change the Contract Length of your clients</summary>
     *
     * Valid values: 1, 12 (months)
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>contractLength Contract length, valid values: 1, 12</param>
	 * <returns>object Returns true on success</returns>
	 */
    	public object Admin_Change_Account_Contract_Length( int clientId, int contractLength ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("contractLength", contractLength);

    		return transport.MakeCall("Admin_Change_Account_Contract_Length", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Change_Account_Payment_Frequency allows you to change the Payment Frequency of your clients</summary>
     *
     * Valid values: 1, 3, 6, 12 (months)
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>paymentFrequency Payment frequency, valid values: 1, 3, 6, 12</param>
	 * <returns>object Returns true on success</returns>
	 */
    	public object Admin_Change_Account_Payment_Frequency( int clientId, int paymentFrequency ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("paymentFrequency", paymentFrequency);

    		return transport.MakeCall("Admin_Change_Account_Payment_Frequency", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Change_Account_Powered_By allows you to toggle the display of the Powered By logo for your clients</summary>
     *
     * Valid values: true, false
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>boolpoweredBy Display Powered By mail2 logo</param>
	 * <returns>object Returns true on success</returns>
	 */
    	public object Admin_Change_Account_Powered_By( int clientId, bool poweredBy ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("poweredBy", poweredBy);

    		return transport.MakeCall("Admin_Change_Account_Powered_By", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Change_Account_Surveys allows you to enable or disable the Surveys feature for your clients</summary>
     *
     * Valid values: true, false
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>boolsurveys Set to true to enable Surveys, false to disable</param>
	 * <returns>object Returns true on success</returns>
	 */
    	public object Admin_Change_Account_Surveys( int clientId, bool surveys ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("surveys", surveys);

    		return transport.MakeCall("Admin_Change_Account_Surveys", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Add_Block_Sends allows you to add block quota sends to one of your clients</summary>
     *
     * 
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>numSendsToAdd numSendsToAdd</param>
	 * <returns>object Returns true on success</returns>
	 */
    	public object Admin_Add_Block_Sends( int clientId, int numSendsToAdd ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("numSendsToAdd", numSendsToAdd);

    		return transport.MakeCall("Admin_Add_Block_Sends", args);
    	}
    	    		
    	    		
    /**
     * <summary>Suspend an Account you created</summary>
     *
     * 
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <returns>object true on success</returns>
	 */
    	public object Admin_Suspend_Account( int clientId ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);

    		return transport.MakeCall("Admin_Suspend_Account", args);
    	}
    	    		
    	    		
    /**
     * <summary>Reinstate an Account you suspended</summary>
     *
     * 
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <returns>object true on success</returns>
	 */
    	public object Admin_Reinstate_Account( int clientId ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);

    		return transport.MakeCall("Admin_Reinstate_Account", args);
    	}
    	    		
    	    		
    /**
     * <summary>Delete an Account you created</summary>
     *
     * 
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <returns>object true on success</returns>
	 */
    	public object Admin_Delete_Account( int clientId ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);

    		return transport.MakeCall("Admin_Delete_Account", args);
    	}
    	    		
    	    		
    /**
     * <summary>Get a list of all completed campaigns for your clients</summary>
     *
     * 
     *
	 * <param>Dictionary<string, string>optionalParameters Optional Parameters: sortBy, sortDir, startDate, endDate</param>
	 * <returns>object An array of clientIds with a value array containing: campaign_id, campaign_name, campaign_description, start_time, campaign_email_from, campaign_email_from_alias, campaign_email_subject</returns>
	 */
    	public object Admin_Get_Accounts_Completed_Campaigns( Dictionary<string, string> optionalParameters ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("optionalParameters", optionalParameters);

    		return transport.MakeCall("Admin_Get_Accounts_Completed_Campaigns", args);
    	}
    	    		
    	    		
    /**
     * <summary>Get the total number of sends for each of your accounts</summary>
     *
     * 
     *
	 * <param>Dictionary<string, string>optionalParameters Optional Parameters: startDate, endDate</param>
	 * <returns>object An array of clientIds each with a value of the total sends</returns>
	 */
    	public object Admin_Get_Accounts_Sends( Dictionary<string, string> optionalParameters ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("optionalParameters", optionalParameters);

    		return transport.MakeCall("Admin_Get_Accounts_Sends", args);
    	}
    	    		
    	    		
    /**
     * <summary>Get the total number of subscribed emails for each list owned by each of your accounts</summary>
     *
     * 
     *
	 * <returns>object A struct of clientIds each with a value struct of listId => count</returns>
	 */
    	public object Admin_Get_Accounts_List_Count(  ) {
    		object data;
    		Arguments args = new Arguments();


    		return transport.MakeCall("Admin_Get_Accounts_List_Count", args);
    	}
    	    		
    	    		
    /**
     * <summary>Get an API key for one of your accounts</summary>
     *
     * 
     *
	 * <param>clientId The client ID number you want an API key for</param>
	 * <returns>object API Key</returns>
	 */
    	public object Admin_Get_Account_Key( int clientId ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);

    		return transport.MakeCall("Admin_Get_Account_Key", args);
    	}
    	    		
    	    		
    /**
     * <summary>Send an Alert Message to a set of your clients</summary>
     *
     * 
     *
	 * <param>List<object>clientIds An array of Client IDs who will receive the message</param>
	 * <param>stringshortMessage A short message, up to 255 characters</param>
	 * <param>Dictionary<string, string>optionalParameters A struct of optional parameters, see below for valid keys</param>
	 */
    	public object Admin_Send_Message( List<object> clientIds, string shortMessage, Dictionary<string, string> optionalParameters ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientIds", clientIds);
			args.Add("shortMessage", shortMessage);
			args.Add("optionalParameters", optionalParameters);

    		return transport.MakeCall("Admin_Send_Message", args);
    	}
    	    		
    	    		
    /**
     * <summary>Get the current Webhooks settings for the specified Account</summary>
     *
     * 
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <returns>object A struct of Webhook data</returns>
	 */
    	public object Admin_Get_Account_Webhooks( int clientId ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);

    		return transport.MakeCall("Admin_Get_Account_Webhooks", args);
    	}
    	    		
    	    		
    /**
     * <summary>Admin_Set_Account_Webhooks allows you to set the Webhooks information for the specified Account</summary>
     *
     * The Account's current settings will be overriden, so specify all the hooks and locations you want
	 * 
	 * Note:  To receive WEBHOOK_BOUNCED or WEBHOOK_CAMPAIGN_CLICKED, you must have the System location active
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <param>stringurl The URL to receive the Webhook</param>
	 * <param>stringwebhooksKey webhookKey The Webhook Key passed along with the data to verify the origin, this is important for security</param>
	 * <param>List<object>hooks An array of events you would like to receive Webfor. Valid values are: WEBHOOK_SUBSCRIBE, WEBHOOK_UNSUBSCRIBE, WEBHOOK_GLOBAL_UNSUBSCRIBE, WEBHOOK_PROFILE_UPDATE, WEBHOOK_BOUNCED, WEBHOOK_EMAIL_CHANGED, WEBHOOK_CAMPAIGN_OPENED, WEBHOOK_CAMPAIGN_CLICKED, WEBHOOK_CAMPAIGN_SENDING_STARTED, WEBHOOK_CAMPAIGN_SENT, WEBHOOK_CAMPAIGN_SENT_TO_ADDITIONAL_RECIPIENT, WEBHOOK_REACTIVATED, WEBHOOK_LIST_CREATED, WEBHOOK_LIST_DELETED</param>
	 * <param>List<object>locations An array of Locations a particular Hook can be fired from. Valid values are: ContactDirect, Webapp, WebappBulk, System, API</param>
	 * <param>List<object>customFieldIds An array of Custom Field IDs (See: Custom_Field_Get_All) - if specified, the values of the specified fields will be included in the data payload for any Webhook sending contact data</param>
	 * <param>List<object>optionalParameters </param>
	 * <returns>object A struct of Webhook data</returns>
	 */
    	public object Admin_Set_Account_Webhooks( int clientId, string url, string webhooksKey, List<object> hooks, List<object> locations, List<object> customFieldIds, List<object> optionalParameters ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("url", url);
			args.Add("webhooksKey", webhooksKey);
			args.Add("hooks", hooks);
			args.Add("locations", locations);
			args.Add("customFieldIds", customFieldIds);
			args.Add("optionalParameters", optionalParameters);

    		return transport.MakeCall("Admin_Set_Account_Webhooks", args);
    	}
    	    		
    	    		
    /**
     * <summary>Deactivate all Webhooks for an account</summary>
     *
     * Note: Webhooks already queued for sending will still be sent
     *
	 * <param>clientId The client ID number you want to modify</param>
	 * <returns>object True on success</returns>
	 */
    	public object Admin_Deactivate_Account_Webhooks( int clientId ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);

    		return transport.MakeCall("Admin_Deactivate_Account_Webhooks", args);
    	}
    	    		
    	    		
    /**
     * <summary>Get Purchase Order data for an account</summary>
     *
     * 
     *
	 * <returns>object A struct of purchase order data</returns>
	 */
    	public object Admin_Get_Purchase_Orders(  ) {
    		object data;
    		Arguments args = new Arguments();


    		return transport.MakeCall("Admin_Get_Purchase_Orders", args);
    	}
    	    		
    	    		
    /**
     * <summary>Get the number of Inbox Analysis Tests remaining for the specified Account</summary>
     *
     * 
     *
	 * <param>clientId The client ID number for which you want the number of remaining tests</param>
	 * <returns>object The number of Inbox Analysis tests available for use by the specified Account</returns>
	 */
    	public object Admin_Get_Inbox_Analysis_Remaining( int clientId ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);

    		return transport.MakeCall("Admin_Get_Inbox_Analysis_Remaining", args);
    	}
    	    		
    	    		
    /**
     * <summary>Find Inbox Analysis Tests used</summary>
     *
     * Defaults to this month if no date parameters are given
     *
	 * <param>clientId The client ID number for which you want the number of remaining tests</param>
	 * <returns>object The Inbox Analysis tests used</returns>
	 */
    	public object Admin_Get_Inbox_Analysis_Tests( int clientId ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);

    		return transport.MakeCall("Admin_Get_Inbox_Analysis_Tests", args);
    	}
    	    		
    	    		
    /**
     * <summary>Purchase additional Inbox Analysis Tests for your the specified Account</summary>
     *
     * Warning: This API call will cause your account to be billed for the tests you grant to the specified account.  Please contact sales@mail2.com for more info.
     *
	 * <param>clientId The client ID number for which you are purchasing tests</param>
	 * <param>numTests The number of tests you are ordering to be added to the specified Account</param>
	 * <returns>object True on success</returns>
	 */
    	public object Admin_Purchase_Inbox_Analysis_Tests( int clientId, int numTests ) {
    		object data;
    		Arguments args = new Arguments();

			args.Add("clientId", clientId);
			args.Add("numTests", numTests);

    		return transport.MakeCall("Admin_Purchase_Inbox_Analysis_Tests", args);
    	}
    	    		
    	    		
    /**
     * <summary>Get Campaign data for an account</summary>
     *
     * 
     *
	 * <returns>object A struct of Campaign data</returns>
	 */
    	public object Admin_Find_Campaigns(  ) {
    		object data;
    		Arguments args = new Arguments();


    		return transport.MakeCall("Admin_Find_Campaigns", args);
    	}
    	
	}
}