using System.Data;
using System;
using Microsoft.ApplicationBlocks.Data;


#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2018
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
//
#endregion


namespace DotNetNuke.Modules.Events
	{
		
		public class SqlDataProvider : DataProvider
		{
			
			
#region Private Data
			private const string ProviderType = "data";
			private Framework.Providers.ProviderConfiguration _providerConfiguration = Framework.Providers.ProviderConfiguration.GetProviderConfiguration(ProviderType);
			private string _connectionString;
			private string _providerPath;
			private string _objectQualifier;
			private string _databaseOwner;
#endregion
			
#region Base Methods
			public SqlDataProvider()
			{
				
				// Read the configuration specific information for this provider
				Framework.Providers.Provider objProvider = (Framework.Providers.Provider) (_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);
				
				// This code handles getting the connection string from either the connectionString / appsetting section and uses the connectionstring section by default if it exists.
				// Get Connection string from web.config
				_connectionString = Common.Utilities.Config.GetConnectionString();
				
				// If above funtion does not return anything then connectionString must be set in the dataprovider section.
				if (string.IsNullOrEmpty(_connectionString))
				{
					// Use connection string specified in provider
					_connectionString = objProvider.Attributes["connectionString"];
				}
				
				_providerPath = objProvider.Attributes["providerPath"];
				
				_objectQualifier = objProvider.Attributes["objectQualifier"];
				if (!string.IsNullOrEmpty(_objectQualifier)&& _objectQualifier.EndsWith("_") == false)
				{
					_objectQualifier += "_";
				}
				
				_databaseOwner = objProvider.Attributes["databaseOwner"];
				if (!string.IsNullOrEmpty(_databaseOwner)&& _databaseOwner.EndsWith(".") == false)
				{
					_databaseOwner += ".";
				}
				
			}
			
			public string ConnectionString
			{
				get
				{
					return _connectionString;
				}
			}
			
			public string ProviderPath
			{
				get
				{
					return _providerPath;
				}
			}
			
			public override string ObjectQualifier
			{
				get
				{
					return _objectQualifier;
				}
			}
			
			public override string DatabaseOwner
			{
				get
				{
					return _databaseOwner;
				}
			}
#endregion
			
#region Event Methods
			
			public override void EventsDelete(int eventId, int moduleId)
			{
				SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsDelete", eventId, moduleId);
			}
			
			public override IDataReader EventsGet(int eventId, int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsGet", eventId, moduleId)));
			}
			
			public override IDataReader EventsGetByRange(string modules, DateTime beginDate, DateTime endDate, string categoryIDs, string locationIDs, int socialGroupId, int socialUserId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsGetByRange", modules, beginDate, endDate, categoryIDs, locationIDs, socialGroupId, socialUserId)));
			}
			
			public override IDataReader EventsSave(int portalId, int eventId, int recurMasterId, int moduleId, DateTime eventTimeBegin, int duration, string eventName, string eventDesc, int importance, string createdById, string notify, bool approved, bool signups, int maxEnrollment, int enrollRoleId, decimal enrollFee, string enrollType, string payPalAccount, bool cancelled, bool detailPage, bool detailNewWin, string detailUrl, string imageUrl, string imageType, int imageWidth, int imageHeight, bool imageDisplay, int location, int category, string reminder, bool sendReminder, int reminderTime, string reminderTimeMeasurement, string reminderFrom, bool searchSubmitted, string customField1, string customField2, bool enrollListView, bool displayEndDate, bool allDayEvent, int ownerId, int lastUpdatedId, DateTime originalDateBegin, bool newEventEmailSent, bool allowAnonEnroll, int contentItemId, bool journalItem, string summary, bool saveOnly)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSave", portalId, eventId, recurMasterId, moduleId, 
					eventTimeBegin, duration, eventName, eventDesc, importance, 
					createdById, notify, approved, signups, maxEnrollment, enrollRoleId, 
					enrollFee, enrollType, payPalAccount, cancelled, detailPage, detailNewWin, detailUrl, 
					imageUrl, imageType, imageWidth, imageHeight, 
					imageDisplay, location, category, 
					reminder, sendReminder, reminderTime, reminderTimeMeasurement, reminderFrom, searchSubmitted, 
					customField1, customField2, enrollListView, displayEndDate, allDayEvent, 
					ownerId, lastUpdatedId, originalDateBegin, newEventEmailSent, allowAnonEnroll, contentItemId, journalItem, summary, saveOnly)));
			}
			
			public override IDataReader EventsModerateEvents(int moduleId, int socialGroupId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsModerateEvents", moduleId, socialGroupId)));
			}
			
			public override int EventsTimeZoneCount(int moduleId)
			{
				return System.Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsTimeZoneCount", moduleId));
			}
			
			public override void EventsUpgrade(string moduleVersion)
			{
				SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsUpgrade", moduleVersion);
			}
			
			public override void EventsCleanupExpired(int portalId, int moduleId)
			{
				SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsCleanupExpired", portalId, moduleId);
			}
			
			public override IDataReader EventsGetRecurrences(int recurMasterId, int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsGetRecurrences", recurMasterId, moduleId)));
			}
			
			
#endregion
			
#region Master Events Methods
			
			public override void EventsMasterDelete(int masterId, int moduleId)
			{
				SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsMasterDelete", masterId, moduleId);
			}
			
			public override IDataReader EventsMasterAssignedModules(int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsMasterAssignedModules", moduleId)));
			}
			
			public override IDataReader EventsMasterAvailableModules(int portalId, int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsMasterAvailableModules", portalId, moduleId)));
			}
			
			public override IDataReader EventsMasterGet(int moduleId, int subEventId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsMasterGet", moduleId, subEventId)));
			}
			
			public override IDataReader EventsMasterSave(int masterId, int moduleId, int subEventId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsMasterSave", masterId, moduleId, subEventId)));
			}
			
#endregion
			
#region Events Signup Methods
			public override void EventsSignupsDelete(int signupId, int moduleId)
			{
				SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSignupsDelete", signupId, moduleId);
			}
			
			public override IDataReader EventsSignupsGet(int signupId, int moduleId, bool ppipn)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSignupsGet", signupId, moduleId, ppipn)));
			}
			
			public override IDataReader EventsSignupsGetEvent(int eventId, int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSignupsGetEvent", eventId, moduleId)));
			}
			
			public override IDataReader EventsSignupsGetEventRecurMaster(int recurMasterId, int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSignupsGetEventRecurMaster", recurMasterId, moduleId)));
			}
			
			public override IDataReader EventsSignupsGetUser(int eventId, int userId, int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSignupsGetUser", eventId, userId, moduleId)));
			}
			
			public override IDataReader EventsSignupsGetAnonUser(int eventId, string anonEmail, int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSignupsGetAnonUser", eventId, anonEmail, moduleId)));
			}
			
			public override IDataReader EventsSignupsMyEnrollments(int moduleId, int userId, int socialGroupId, string categoryIDs, DateTime beginDate, DateTime endDate)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSignupsMyEnrollments", moduleId, userId, socialGroupId, categoryIDs, beginDate, endDate)));
			}
			
			public override IDataReader EventsSignupsSave(int eventId, int signupId, int moduleId, int userId, bool approved, string 
				payPalStatus, string payPalReason, string payPalTransId, string payPalPayerId, string payPalPayerStatus, 
				string payPalRecieverEmail, string payPalUserEmail, string payPalPayerEmail, 
				string payPalFirstName, string payPalLastName, string payPalAddress, string payPalCity, 
				string payPalState, string payPalZip, string payPalCountry, string payPalCurrency, 
				DateTime payPalPaymentDate, decimal payPalAmount, decimal payPalFee, 
				int noEnrolees, string anonEmail, string anonName, string anonTelephone, string anonCulture, 
				string anonTimeZoneId, string firstName, string lastName, string company, 
				string jobTitle, string referenceNumber, string street, 
				string postalCode, string city, string region, string country)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSignupsSave", 
					eventId, signupId, moduleId, userId, approved, 
					payPalStatus, payPalReason, payPalTransId, payPalPayerId, 
					payPalPayerStatus, payPalRecieverEmail, payPalUserEmail, 
					payPalPayerEmail, payPalFirstName, payPalLastName, payPalAddress, 
					payPalCity, payPalState, payPalZip, payPalCountry, 
					payPalCurrency, payPalPaymentDate, payPalAmount, payPalFee, noEnrolees, anonEmail, anonName, anonTelephone, 
					anonCulture, anonTimeZoneId, firstName, lastName, company, jobTitle, referenceNumber, 
					street, postalCode, city, region, country)));
			}
			
			public override IDataReader EventsModerateSignups(int moduleId, int socialGroupId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsModerateSignups", moduleId, socialGroupId)));
			}
			
			
#endregion
			
#region Events Category and Location Methods
			public override void EventsCategoryDelete(int category, int portalId)
			{
				SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsCategoryDelete", category, portalId);
			}
			
			public override IDataReader EventsCategoryGet(int category, int portalId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsCategoryGet", category, portalId)));
			}
			
			public override IDataReader EventsCategoryGetByName(string categoryName, int portalId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsCategoryGetByName", categoryName, portalId)));
			}
			
			public override IDataReader EventsCategoryList(int portalId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsCategoryList", portalId)));
			}
			
			public override IDataReader EventsCategorySave(int portalId, int category, string 
				categoryName, string color, string fontColor)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsCategorySave", portalId, category, 
					categoryName, color, fontColor)));
			}
			
			public override void EventsLocationDelete(int location, int portalId)
			{
				SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsLocationDelete", location, portalId);
			}
			
			public override IDataReader EventsLocationGet(int location, int portalId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsLocationGet", location, portalId)));
			}
			
			public override IDataReader EventsLocationGetByName(string locationName, int portalId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsLocationGetByName", locationName, portalId)));
			}
			
			public override IDataReader EventsLocationList(int portalId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsLocationList", portalId)));
			}
			
			public override IDataReader EventsLocationSave(int portalId, int location, string 
				locationName, string mapUrl, string street, 
				string postalCode, string city, string region, 
				string country)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsLocationSave", portalId, location, 
					locationName, mapUrl, street, postalCode, city, region, country)));
			}
#endregion
			
#region PPErrorLog Methods
			
			public override IDataReader EventsPpErrorLogAdd(int signupId, string 
				payPalStatus, string payPalReason, string payPalTransId, string payPalPayerId, string payPalPayerStatus, 
				string payPalRecieverEmail, string payPalUserEmail, string payPalPayerEmail, 
				string payPalFirstName, string payPalLastName, string payPalAddress, string payPalCity, 
				string payPalState, string payPalZip, string payPalCountry, string payPalCurrency, 
				DateTime payPalPaymentDate, double payPalAmount, double payPalFee)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsPPErrorLogAdd", 
					signupId, 
					payPalStatus, payPalReason, payPalTransId, payPalPayerId, 
					payPalPayerStatus, payPalRecieverEmail, payPalUserEmail, 
					payPalPayerEmail, payPalFirstName, payPalLastName, payPalAddress, 
					payPalCity, payPalState, payPalZip, payPalCountry, 
					payPalCurrency, payPalPaymentDate, payPalAmount, payPalFee)));
			}
			
#endregion
			
#region Events Notification Methods
			public override void EventsNotificationTimeChange(int eventId, DateTime eventTimeBegin, int moduleId)
			{
				SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsNotificationTimeChange", eventId, eventTimeBegin, moduleId);
			}
			
			public override void EventsNotificationDelete(DateTime deleteDate)
			{
				SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsNotificationDelete", deleteDate);
			}
			
			public override IDataReader EventsNotificationGet(int eventId, string userEmail, int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsNotificationGet", eventId, userEmail, moduleId)));
			}
			
			public override IDataReader EventsNotificationsToSend(DateTime notifyTime)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsNotificationsToSend", notifyTime)));
			}
			
			public override IDataReader EventsNotificationSave(int notificationId, int eventId, int portalAliasId, string userEmail, bool notificationSent, DateTime notifyByDateTime, DateTime eventTimeBegin, string notifyLanguage, int moduleId, int tabId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsNotificationSave", notificationId, eventId, portalAliasId, userEmail, notificationSent, notifyByDateTime, eventTimeBegin, notifyLanguage, moduleId, tabId)));
			}
			
#endregion
			
#region Event Recur Master Methods
			public override void EventsRecurMasterDelete(int recurMasterId, int moduleId)
			{
				SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsRecurMasterDelete", recurMasterId, moduleId);
			}
			
			public override IDataReader EventsRecurMasterGet(int recurMasterId, int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsRecurMasterGet", recurMasterId, moduleId)));
			}
			
			public override IDataReader EventsRecurMasterSave(int recurMasterId, int moduleId, int portalId, string rrule, DateTime dtstart, string duration, DateTime Until, string eventName, string eventDesc, int importance, string notify, bool approved, bool signups, int maxEnrollment, int enrollRoleId, decimal enrollFee, string enrollType, string payPalAccount, bool detailPage, bool detailNewWin, string detailUrl, string imageUrl, string imageType, int imageWidth, int imageHeight, bool imageDisplay, int location, int category, string reminder, bool sendReminder, int reminderTime, string reminderTimeMeasurement, string reminderFrom, string customField1, string customField2, bool enrollListView, bool displayEndDate, bool allDayEvent, string cultureName, int ownerId, int createdById, int updatedById, string eventTimeZoneId, bool allowAnonEnroll, int contentItemId, int socialGroupId, int socialUserId, string summary)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsRecurMasterSave", recurMasterId, moduleId, portalId, rrule, 
					dtstart, duration, Until, eventName, eventDesc, importance, notify, approved, 
					signups, maxEnrollment, enrollRoleId, 
					enrollFee, enrollType, payPalAccount, detailPage, detailNewWin, detailUrl, imageUrl, imageType, imageWidth, imageHeight, 
					imageDisplay, location, category, reminder, sendReminder, reminderTime, 
					reminderTimeMeasurement, reminderFrom, customField1, customField2, enrollListView, displayEndDate, allDayEvent, cultureName, ownerId, 
					createdById, updatedById, eventTimeZoneId, allowAnonEnroll, contentItemId, socialGroupId, socialUserId, summary)));
			}
			
			public override IDataReader EventsRecurMasterModerate(int moduleId, int socialGroupId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsRecurMasterModerate", moduleId, socialGroupId)));
			}
			
			
#endregion
			
#region Event Subscription Methods
			public override void EventsSubscriptionDeleteUser(int userId, int moduleId)
			{
				SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSubscriptionDeleteUser", userId, moduleId);
			}
			
			public override IDataReader EventsSubscriptionGetUser(int userId, int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSubscriptionGetUser", userId, moduleId)));
			}
			
			public override IDataReader EventsSubscriptionGetModule(int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSubscriptionGetModule", moduleId)));
			}
			
			public override IDataReader EventsSubscriptionGetSubModule(int moduleId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSubscriptionGetSubModule", moduleId)));
			}
			
			public override IDataReader EventsSubscriptionSave(int subscriptionId, int moduleId, int portalId, int userId)
			{
				return ((IDataReader) (SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner + ObjectQualifier + "EventsSubscriptionSave", subscriptionId, moduleId, portalId, userId)));
			}
			
#endregion
		}
		
	}
