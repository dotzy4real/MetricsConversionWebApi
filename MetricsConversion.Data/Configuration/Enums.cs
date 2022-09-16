
namespace MetricsConversion.Data.Configuration
{

    public enum DeliveryBatchUsage { 
    
        Dispatch = 5,

        LogReco = 10,

        FinReco = 15
    }

    public enum RecoStatus
    {

        Open = 5,

        Processing = 10,

        Completed = 15,

        ForcedClosedProcessing = 20,

        ForcedClosed = 25

    }

    public enum DeliveryStatus
    {
        NotYetSynched = 0,

        Rescheduled = 5,

        PartiallyDelivered = 10,

        FullyDelivered = 15,

        FullyRejected = 20
    }

    public enum OrderMessage
    { 
        FirstOrder = 5,

        NewOrderCreation = 10,

        OrderPackage = 15,

        OrderShipment = 20,

        OrderDelivery = 25
    }
    public enum Tracker
    {
        not_started,
        new_request,
        processing,
        done,
        error,
        synced

    };

    public enum WarehouseSynchronizationMode { 
    
        Single = 5,

        Multiple = 10
    }


    public enum eCommerceObject
    {
        Product = 1,
        SalesOrder = 2,
        Invoice = 3,
        Package = 5,
        Shipment = 10,
        Payment = 15,
    }

    public enum OrderProcess { 

        BcOrderCreation = 1,

        ZohoOrderSynchronization = 2,

        ZohoOrderCreation = 3,

        WarehouseAppOrderDataPush = 4,

        ZohoPackageRequest = 5,

        ZohoPackageCreation = 6,

        ZohoInvoiceRequest = 7,

        ZohoInvoiceCreation = 8,

        ZohoShipmentCreation = 9,

        DispatchSynchronization = 10,

        ZohoShipmentRequest = 11,

        ZohoPackageDeletionRequest = 12,

        ZohoPackageDeletion = 13,

        ZohoShipmentDeletionReqeust = 14,

        ZohoShipmentDeletion = 15,

        ZohoShipmentDelveryRequest = 16,

        ZohoShipmentDelivery = 17,

        ZohoShipmentRejectionRequest = 18,

        ZohoShipmentRejection = 19,

        ZohoSalesReturnRequest = 20,

        ZohoSalesReturn = 21,

        ZohoPayment = 22,

        BcOrderCancellation = 23,

        BcOrderReview = 24


    }


    public enum SOShipmentStatus { 
        
        Rejection = 5,

        PartialDelivery = 10,

        FullDelivery = 15

    }

    public enum BCOrderStatus
    {

        ForReview = 0,
        Pending = 1,
        Shipped = 2,
        PartiallyShipped = 3,
        Refunded = 4,
        Cancelled = 5,
        Declined = 6,
        AwaitingPayment = 7,
        AwaitingPickup = 8,
        AwaitingShipment = 9,
        Completed = 10,
        AwaitingFulfillment = 11,
        PartiallyRefunded = 12,
        ManualVerificationRequired = 13,
        PayLaterApproved = 14

    }



    public enum SOProcess { 
        
        ZohoSynchronization = 1,
        WhAppSynchronization = 2,
        PackageCreation = 3,
        InvoiceCreation = 4,
        PackageUpdate = 5,
        DispatchSynchronization = 6,
        ShipmentCreation = 7,
        PaymentCreation = 8,
        ShipmentDelivery = 9,
        SalesReturn = 10,
        ShipmentRejection = 11,

        PackageCreationRequest = 20,
        InvoiceCreationRequest = 25,
        ShipmentCreationRequest = 30,
        PaymentRequest = 35,
        ShipmentDeliveryRequest = 40,
        SalesReturnRequest = 45,
        ShipmentRejectionRequest = 50

    }
    public enum SOProcessStatus { 
        
        ToStart = 0,

        Done = 1,

        Error = -1,

        Deleted = 2
    }

    public enum RecordStatus { 
        
        Active  = 1,

        InActive = 0,

        Deleted =  -1
        
    }

    public enum TransactionType { 
        
        Credit = 5,

        Debit = 10
    
    }

    public enum PaymentMethod { 
    
        Cash = 5,

        POS = 10,

        BankTransfer = 15,

        WalletPayment = 20,

        Default = 25
    }

    public enum ExternalSource { 
        
        SimpleMarket = 5
    }

    public enum WalletType {
        
        ConsumerOfGoods = 1,

        ProducerOfGoods = 2
    }
    public enum EntityAction { 
    
        Create = 5,
        Update = 10,
        Delete = 15
    }

    public enum LoginStatus { 
       
        Successful = 5,

        Failed = 10
    }

    public enum MessageType { 
        Email = 5,
        SMS = 10,
        Bots = 15,
        PDF = 20
    }

    public enum PasswordFormat
    {
        Clear = 0,
        Hashed = 1,
        Encrypted = 2
    }

   
    public enum StoreFrontUserType
    {
        System = 0,
        Staff = 1,
        Agent = 2,
        Customer = 3
    }

    public enum StoreFrontCUstomerTypeStatus
    {
        New = 5,
        Existing = 10
    }

    public enum StoreFrontCustomerActivityStatus
    {
        InActive = 5,
        ActivationInProgress = 10,
        Active = 15
    }


    public enum AccountType
    {
        Guest = 0,
        Administrator = 15,
        SuperAdministrator = 25,
        AdministratorWithMobileAccess = 30,
        CorporateUser = 40
    }


    /// <summary>
    /// Represents the user registration type fortatting enumeration
    /// </summary>
    public enum UserRegistrationType
    {
        /// <summary>
        /// Standard account creation
        /// </summary>
        Standard = 1,
        /// <summary>
        /// Email validation is required after registration
        /// </summary>
        EmailValidation = 2,
        /// <summary>
        /// A user should be approved by administrator
        /// </summary>
        AdminApproval = 3,
        /// <summary>
        /// Registration is disabled
        /// </summary>
        Disabled = 4,
    }

    /// <summary>
    /// Represents the user name fortatting enumeration
    /// </summary>
    public enum UserNameFormat
    {
        /// <summary>
        /// Show emails
        /// </summary>
        ShowEmails = 1,
        /// <summary>
        /// Show usernames
        /// </summary>
        ShowUsernames = 2,
        /// <summary>
        /// Show full names
        /// </summary>
        ShowFullNames = 3,
        /// <summary>
        /// Show first name
        /// </summary>
        ShowFirstName = 10
    }

   
    public enum ThreadStatus
    {
        NewMessage = 1,
        Reply = 2
    }

    public enum ThreadOption
    {
        Reply = 1,
        Forward = 2
    }


    //AfricaTalking and Termii

    public enum SMSGatewayProvider
    {
        AfricasTalking = 5,

        Termii = 10

    }


    public enum SMSStatus
    {
        Unknown = 0,

        Sent = 1,

        Submitted = 2,

        Buffered = 3,

        Rejected = 4,

        Success = 5,

        Failed = 6,

    }


    public enum NotMessageType
    {
        SMS = 1,
        /// <summary>
        /// Email
        /// </summary>
        eMail = 2,
        /// <summary>
        /// Internal Message
        /// </summary>
        Internal = 3,
    }

    public enum NotificationOption
    {
        Hours = 5,

        Days = 10
    }

    public enum NotificationCondition
    {
        Before = 5,

        After = 10
    }

    public enum NotificationTarget
    {
        Customer = 5,

        Staff = 10
    }

    public enum NotificationField
    {
        Birthday = 5,

        WeddingAnniversary = 10,

        FeeDueDate = 15,

        SpecificDate = 20
    }

   
  
    public enum AddressType {
        HomeAddress = 1,
        BusinessAddress = 2,
        EmployerAddress = 3,
        CorporateAddress =5,
        BillingAddress = 10,
        ShippingAddress = 15,
        WarehouseAddress = 20,
        StoreAddress = 25,
        PackageBillingAddress = 30,
        PackageShippingAddress = 35,
        WarehouseBillingAddress = 40,
        WarehouseDeliveryAddress = 45
    }


    public enum ExternalAppSource
    {
        BigCommerce = 5,
        Shopify = 10,
        InternalApp = 15,
        Zoho = 20,
        WarehouseAppV1 = 25,
        WarehouseAppV2 = 30,
        StoreFront = 35,
        WarehouseAppV3 = 40,
    }


    public enum OrderType { 
        PayLater = 5,
        PayOnDelivery = 10
    }

    public enum APIVersion { 
        v1 = 1,
        v2 = 2
    }

    public enum CustomerSource
    {
        BigCommerce = 5,
        FieldApp = 10
    }


    public enum CustomerPreference
    {
        Primary = 5,
        Others = 10
    }


    public enum OrderOperation
    {
        Open = 5,

        Imported = 10,

        Processing = 15,

        Processed = 20,

        Initialized = 25,

        Cancelled = 30
    }

    public enum BlacklistViewOptions
    {
        BlackListedView = 1,
        NotBlackListedView = 2,
        CombinedView = 0
    }

    public enum BlacklistReasons
    {
        Null = 0,
        LatePayment = 1,
        CounterfeitMoney = 2,
        UnwillingToPay = 3
    }

    /// <summary>
    /// Rounding type
    /// </summary>
    public enum RoundingType
    {
        /// <summary>
        /// Default rounding (Match.Round(num, 2))
        /// </summary>
        Rounding001 = 0,

        /// <summary>
        /// <![CDATA[Prices are rounded up to the nearest multiple of 5 cents for sales ending in: 3¢ & 4¢ round to 5¢; and, 8¢ & 9¢ round to 10¢]]>
        /// </summary>
        Rounding005Up = 10,

        /// <summary>
        /// <![CDATA[Prices are rounded down to the nearest multiple of 5 cents for sales ending in: 1¢ & 2¢ to 0¢; and, 6¢ & 7¢ to 5¢]]>
        /// </summary>
        Rounding005Down = 20,

        /// <summary>
        /// <![CDATA[Round up to the nearest 10 cent value for sales ending in 5¢]]>
        /// </summary>
        Rounding01Up = 30,

        /// <summary>
        /// <![CDATA[Round down to the nearest 10 cent value for sales ending in 5¢]]>
        /// </summary>
        Rounding01Down = 40,

        /// <summary>
        /// <![CDATA[Sales ending in 1–24 cents round down to 0¢
        /// Sales ending in 25–49 cents round up to 50¢
        /// Sales ending in 51–74 cents round down to 50¢
        /// Sales ending in 75–99 cents round up to the next whole dollar]]>
        /// </summary>
        Rounding05 = 50,

        /// <summary>
        /// Sales ending in 1–49 cents round down to 0
        /// Sales ending in 50–99 cents round up to the next whole dollar
        /// For example, Swedish Krona
        /// </summary>
        Rounding1 = 60,

        /// <summary>
        /// Sales ending in 1–99 cents round up to the next whole dollar
        /// </summary>
        Rounding1Up = 70
    }

    public enum SalesSynchStatus
    {

        InvoiceOK = 5,

        InvoiceNotCreated = 10,

        InvoiceCreated = 15,

        InvoiceError = 20,

        PackageOK = 25,

        PackageNotCreated = 30,

        PackageCreated = 35,

        PackageError = 40,

        ShipmentOK = 45,

        ShipmentNotCreated = 50,

        ShipmentCreated = 55,

        ShipmentError = 60,


        DeliveryOK = 65,

        DeliveryNotCreated = 70,

        DeliveryCreated = 75,

        DeliveryError = 80,


        SalesReturnOK = 85,

        SalesReturnNotCreated = 90,

        SalesReturnCreated = 95,

        SalesReturnError = 100,


        PaymentOK = 105,

        PaymentNotCreated = 110,

        PaymentCreated = 115,

        PaymentError = 120,

        OrderCreated = 125,

        OrderCreationError = 130,

        OrderUpdated = 135,

        OrderUpdateError = 140

    }

    public enum OrderStatus { 
        Migrated = 0,

        New = 5,

        SynchedToPick = 10,

        SynchedToDispatch = 15
    }

    public enum ProcessStatus {

        Migrated = 0,

        New = 5,

        Processing = 10,


        Error = 15,

        Completed = 20,

        Deleted = 30

    }

    public enum ReverseRequestType
    {
        PartialToFull = 5,
        FullToRejected = 10,
        PartialToRejected = 15,
        RescheduledToRejected = 20,
        FullToPartial = 25,
        RescheduledToPartial = 30,
        FullToRescheduled = 35,
        PartialToRescheduled = 40
    }

    public enum WarehouseExceptionStatus
    {
        New = 5,

        Processing = 10,

        Error = 15,

        Completed = 20,
    }

    public enum StaggingStatus
    {
        Migrated = 0,

        New = 5,

        Processing = 10,


        Error = 15,

        Completed = 20,

        Deleted = 30,

        Reversed = 35,

        ReversedBeforeSynch = 40

    }

    public enum StoreFrontOrderCancellationStatus{
        NotCancelRequest = 0,
        NewCancelRequest = 1,
        CancellingInProgress = 5,
        CancellingError = 10,
        CancelledComplete = 15
        
    }

    public enum StoreFrontStatus {

        NewRequest = 1,

        ToDo = 2,
        
        FullyPlaced = 3,
        
        PartiallyPlaced = 4,
        
        ErrorOnPlacement = 5,
        
        Shipped = 6,
        
        Cancelled = 7,
        
        UnderReview = 8,
        
        Processing = 9,
        
        Reviewed = 10,
        
        ErrorOnReview = 11,
        
        CancelledOnReviewed = 12,

        PlacementProcessing = 13

    }

    public enum ShipmentStatus
    { 
        New = 5,

        ReAssign = 10
    }

    public enum BlacklistAuditAction
    {
        Blacklist = 1,
        RemovedFromBlacklist = 2
    }
}
