using System;
using System.Collections;

namespace Glaer.Trade.B2C.ORM
{
    public class Mapping
    {
        public Hashtable Relation;
        public Mapping()
        {
            Relation = new Hashtable();

            //系统配置
            Relation["ConfigInfo.Sys_Config_ID"] = "Sys_Config.Sys_Config_ID";
            Relation["ConfigInfo.Site_DomainName"] = "Sys_Config.Site_DomainName";
            Relation["ConfigInfo.Site_Name"] = "Sys_Config.Site_Name";
            Relation["ConfigInfo.Site_URL"] = "Sys_Config.Site_URL";
            Relation["ConfigInfo.Sys_Config_Site"] = "Sys_Config.Sys_Config_Site";

            //Category
            Relation["CategoryInfo.Cate_ID"] = "Category.Cate_ID";
            Relation["CategoryInfo.Cate_ParentID"] = "Category.Cate_ParentID";
            Relation["CategoryInfo.Cate_Name"] = "Category.Cate_Name";
            Relation["CategoryInfo.Cate_TypeID"] = "Category.Cate_TypeID";
            Relation["CategoryInfo.Cate_Img"] = "Category.Cate_Img";
            Relation["CategoryInfo.Cate_ProductTypeID"] = "Category.Cate_ProductTypeID";
            Relation["CategoryInfo.Cate_Sort"] = "Category.Cate_Sort";
            Relation["CategoryInfo.Cate_IsFrequently"] = "Category.Cate_IsFrequently";
            Relation["CategoryInfo.Cate_IsActive"] = "Category.Cate_IsActive";
            Relation["CategoryInfo.Cate_Count_Brand"] = "Category.Cate_Count_Brand";
            Relation["CategoryInfo.Cate_Count_Product"] = "Category.Cate_Count_Product";
            Relation["CategoryInfo.Cate_SEO_Path"] = "Category.Cate_SEO_Path";
            Relation["CategoryInfo.Cate_SEO_Title"] = "Category.Cate_SEO_Title";
            Relation["CategoryInfo.Cate_SEO_Keyword"] = "Category.Cate_SEO_Keyword";
            Relation["CategoryInfo.Cate_SEO_Description"] = "Category.Cate_SEO_Description";
            Relation["CategoryInfo.Cate_Site"] = "Category.Cate_Site";
            Relation["CategoryInfo.Cate_Number"] = "Category.Cate_Number";

            //Brand
            Relation["BrandInfo.Brand_ID"] = "Brand.Brand_ID";
            Relation["BrandInfo.Brand_Name"] = "Brand.Brand_Name";
            Relation["BrandInfo.Brand_Img"] = "Brand.Brand_Img";
            Relation["BrandInfo.Brand_URL"] = "Brand.Brand_URL";
            Relation["BrandInfo.Brand_Description"] = "Brand.Brand_Description";
            Relation["BrandInfo.Brand_Sort"] = "Brand.Brand_Sort";
            Relation["BrandInfo.Brand_IsRecommend"] = "Brand.Brand_IsRecommend";
            Relation["BrandInfo.Brand_IsActive"] = "Brand.Brand_IsActive";
            Relation["BrandInfo.Brand_Site"] = "Brand.Brand_Site";

            //ProductType
            Relation["ProductType.ProductType_ID"] = "ProductType.ProductType_ID";
            Relation["ProductType.ProductType_Name"] = "ProductType.ProductType_Name";
            Relation["ProductType.ProductType_Sort"] = "ProductType.ProductType_Sort";
            Relation["ProductType.ProductType_IsActive"] = "ProductType.ProductType_IsActive";
            Relation["ProductType.ProductType_IsReception"] = "ProductType.ProductType_IsReception";
            Relation["ProductType.ProductType_Site"] = "ProductType.ProductType_Site";

            //ProductType_Extend
            Relation["ProductTypeExtendInfo.ProductType_Extend_ID"] = "ProductType_Extend.ProductType_Extend_ID";
            Relation["ProductTypeExtendInfo.ProductType_Extend_ProductTypeID"] = "ProductType_Extend.ProductType_Extend_ProductTypeID";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Name"] = "ProductType_Extend.ProductType_Extend_Name";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Display"] = "ProductType_Extend.ProductType_Extend_Display";
            Relation["ProductTypeExtendInfo.ProductType_Extend_IsSearch"] = "ProductType_Extend.ProductType_Extend_IsSearch";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Options"] = "ProductType_Extend.ProductType_Extend_Options";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Default"] = "ProductType_Extend.ProductType_Extend_Default";
            Relation["ProductTypeExtendInfo.ProductType_Extend_IsActive"] = "ProductType_Extend.ProductType_Extend_IsActive";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Sort"] = "ProductType_Extend.ProductType_Extend_Sort";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Site"] = "ProductType_Extend.ProductType_Extend_Site";

            //Product_Review
            Relation["ProductReviewInfo.Product_Review_ID"] = "Product_Review.Product_Review_ID";
            Relation["ProductReviewInfo.Product_Review_ProductID"] = "Product_Review.Product_Review_ProductID";
            Relation["ProductReviewInfo.Product_Review_MemberID"] = "Product_Review.Product_Review_MemberID";
            Relation["ProductReviewInfo.Product_Review_Star"] = "Product_Review.Product_Review_Star";
            Relation["ProductReviewInfo.Product_Review_Subject"] = "Product_Review.Product_Review_Subject";
            Relation["ProductReviewInfo.Product_Review_Content"] = "Product_Review.Product_Review_Content";
            Relation["ProductReviewInfo.Product_Review_Useful"] = "Product_Review.Product_Review_Useful";
            Relation["ProductReviewInfo.Product_Review_Useless"] = "Product_Review.Product_Review_Useless";
            Relation["ProductReviewInfo.Product_Review_Addtime"] = "Product_Review.Product_Review_Addtime";
            Relation["ProductReviewInfo.Product_Review_IsShow"] = "Product_Review.Product_Review_IsShow";
            Relation["ProductReviewInfo.Product_Review_IsBuy"] = "Product_Review.Product_Review_IsBuy";
            Relation["ProductReviewInfo.Product_Review_IsGift"] = "Product_Review.Product_Review_IsGift";
            Relation["ProductReviewInfo.Product_Review_IsView"] = "Product_Review.Product_Review_IsView";
            Relation["ProductReviewInfo.Product_Review_IsRecommend"] = "Product_Review.Product_Review_IsRecommend";
            Relation["ProductReviewInfo.Product_Review_Site"] = "Product_Review.Product_Review_Site";
            Relation["ProductReviewInfo.Product_Review_Buttime"] = "Product_Review.Product_Review_Buttime";
            Relation["ProductReviewInfo.Product_Review_Type"] = "Product_Review.Product_Review_Type";

            //Product_Review_Config
            Relation["ProductReviewConfigInfo.Product_Review_Config_ID"] = "Product_Review_Config.Product_Review_Config_ID";
            Relation["ProductReviewConfigInfo.Product_Review_Config_ProductCount"] = "Product_Review_Config.Product_Review_Config_ProductCount";
            Relation["ProductReviewConfigInfo.Product_Review_Config_ListCount"] = "Product_Review_Config.Product_Review_Config_ListCount";
            Relation["ProductReviewConfigInfo.Product_Review_Config_Power"] = "Product_Review_Config.Product_Review_Config_Power";
            Relation["ProductReviewConfigInfo.Product_Review_giftcoin"] = "Product_Review_Config.Product_Review_giftcoin";
            Relation["ProductReviewConfigInfo.Product_Review_Recommendcoin"] = "Product_Review_Config.Product_Review_Recommendcoin";
            Relation["ProductReviewConfigInfo.Product_Review_Config_NoRecordTip"] = "Product_Review_Config.Product_Review_Config_NoRecordTip";
            Relation["ProductReviewConfigInfo.Product_Review_Config_VerifyCode_IsOpen"] = "Product_Review_Config.Product_Review_Config_VerifyCode_IsOpen";
            Relation["ProductReviewConfigInfo.Product_Review_Config_ManagerReply_Show"] = "Product_Review_Config.Product_Review_Config_ManagerReply_Show";
            Relation["ProductReviewConfigInfo.Product_Review_Config_Show_SuccessTip"] = "Product_Review_Config.Product_Review_Config_Show_SuccessTip";
            Relation["ProductReviewConfigInfo.Product_Review_Config_IsActive"] = "Product_Review_Config.Product_Review_Config_IsActive";
            Relation["ProductReviewConfigInfo.Product_Review_Config_Site"] = "Product_Review_Config.Product_Review_Config_Site";

            //product_tag
            Relation["ProductTagInfo.Product_Tag_ID"] = "product_tag.Product_Tag_ID";
            Relation["ProductTagInfo.Product_Tag_Cate_ID"] = "product_tag.Product_Tag_Cate_ID";
            Relation["ProductTagInfo.Product_Tag_Name"] = "product_tag.Product_Tag_Name";
            Relation["ProductTagInfo.Product_Tag_IsActive"] = "product_tag.Product_Tag_IsActive";
            Relation["ProductTagInfo.Product_Tag_Site"] = "product_tag.Product_Tag_Site";

            //Product_Tag_Category
            Relation["ProductTagCategoryInfo.Tag_Cate_ID"] = "Product_Tag_Category.Tag_Cate_ID";
            Relation["ProductTagCategoryInfo.Tag_Cate_Name"] = "Product_Tag_Category.Tag_Cate_Name";
            Relation["ProductTagCategoryInfo.Tag_Cate_IsActive"] = "Product_Tag_Category.Tag_Cate_IsActive";
            Relation["ProductTagCategoryInfo.Tag_Cate_Sort"] = "Product_Tag_Category.Tag_Cate_Sort";
            Relation["ProductTagCategoryInfo.Tag_Cate_Site"] = "Product_Tag_Category.Tag_Cate_Site";

            //Product_Basic
            Relation["ProductInfo.Product_ID"] = "Product_Basic.Product_ID";
            Relation["ProductInfo.Product_Code"] = "Product_Basic.Product_Code";
            Relation["ProductInfo.Product_CateID"] = "Product_Basic.Product_CateID";
            Relation["ProductInfo.Product_BrandID"] = "Product_Basic.Product_BrandID";
            Relation["ProductInfo.Product_TypeID"] = "Product_Basic.Product_TypeID";
            Relation["ProductInfo.Product_SupplierID"] = "Product_Basic.Product_SupplierID";
            Relation["ProductInfo.Product_Supplier_CommissionCateID"] = "Product_Basic.Product_Supplier_CommissionCateID";
            Relation["ProductInfo.Product_Name"] = "Product_Basic.Product_Name";
            Relation["ProductInfo.Product_NameInitials"] = "Product_Basic.Product_NameInitials";
            Relation["ProductInfo.Product_SubName"] = "Product_Basic.Product_SubName";
            Relation["ProductInfo.Product_SubNameInitials"] = "Product_Basic.Product_SubNameInitials";
            Relation["ProductInfo.Product_MKTPrice"] = "Product_Basic.Product_MKTPrice";
            Relation["ProductInfo.Product_GroupPrice"] = "Product_Basic.Product_GroupPrice";
            Relation["ProductInfo.Product_PurchasingPrice"] = "Product_Basic.Product_PurchasingPrice";
            Relation["ProductInfo.Product_Price"] = "Product_Basic.Product_Price";
            Relation["ProductInfo.Product_PriceUnit"] = "Product_Basic.Product_PriceUnit";
            Relation["ProductInfo.Product_Unit"] = "Product_Basic.Product_Unit";
            Relation["ProductInfo.Product_GroupNum"] = "Product_Basic.Product_GroupNum";
            Relation["ProductInfo.Product_Note"] = "Product_Basic.Product_Note";
            Relation["ProductInfo.Product_Weight"] = "Product_Basic.Product_Weight";
            Relation["ProductInfo.Product_Img"] = "Product_Basic.Product_Img";
            Relation["ProductInfo.Product_Publisher"] = "Product_Basic.Product_Publisher";
            Relation["ProductInfo.Product_StockAmount"] = "Product_Basic.Product_StockAmount";
            Relation["ProductInfo.Product_SaleAmount"] = "Product_Basic.Product_SaleAmount";
            Relation["ProductInfo.Product_Review_Count"] = "Product_Basic.Product_Review_Count";
            Relation["ProductInfo.Product_Review_ValidCount"] = "Product_Basic.Product_Review_ValidCount";
            Relation["ProductInfo.Product_Review_Average"] = "Product_Basic.Product_Review_Average";
            Relation["ProductInfo.Product_IsInsale"] = "Product_Basic.Product_IsInsale";
            Relation["ProductInfo.Product_IsGroupBuy"] = "Product_Basic.Product_IsGroupBuy";
            Relation["ProductInfo.Product_IsCoinBuy"] = "Product_Basic.Product_IsCoinBuy";
            Relation["ProductInfo.Product_IsFavor"] = "Product_Basic.Product_IsFavor";
            Relation["ProductInfo.Product_IsGift"] = "Product_Basic.Product_IsGift";
            Relation["ProductInfo.Product_IsGiftCoin"] = "Product_Basic.Product_IsGiftCoin";
            Relation["ProductInfo.Product_Gift_Coin"] = "Product_Basic.Product_Gift_Coin";
            Relation["ProductInfo.Product_CoinBuy_Coin"] = "Product_Basic.Product_CoinBuy_Coin";
            Relation["ProductInfo.Product_IsAudit"] = "Product_Basic.Product_IsAudit";
            Relation["ProductInfo.Product_Addtime"] = "Product_Basic.Product_Addtime";
            Relation["ProductInfo.Product_Intro"] = "Product_Basic.Product_Intro";
            Relation["ProductInfo.Product_Parameter"] = "Product_Basic.Product_Parameter";
            Relation["ProductInfo.Product_Intro_Extend1"] = "Product_Basic.Product_Intro_Extend1";
            Relation["ProductInfo.Product_Intro_Extend2"] = "Product_Basic.Product_Intro_Extend2";
            Relation["ProductInfo.Product_DetailTag_1"] = "Product_Basic.Product_DetailTag_1";
            Relation["ProductInfo.Product_DetailTag_2"] = "Product_Basic.Product_DetailTag_2";
            Relation["ProductInfo.Product_DetailTag_3"] = "Product_Basic.Product_DetailTag_3";
            Relation["ProductInfo.Product_DetailTag_4"] = "Product_Basic.Product_DetailTag_4";
            Relation["ProductInfo.Product_Trace_Intro"] = "Product_Basic.Product_Trace_Intro";
            Relation["ProductInfo.Product_AlertAmount"] = "Product_Basic.Product_AlertAmount";
            Relation["ProductInfo.Product_UsableAmount"] = "Product_Basic.Product_UsableAmount";
            Relation["ProductInfo.Product_IsNoStock"] = "Product_Basic.Product_IsNoStock";
            Relation["ProductInfo.Product_Spec"] = "Product_Basic.Product_Spec";
            Relation["ProductInfo.Product_Maker"] = "Product_Basic.Product_Maker";
            Relation["ProductInfo.Product_Sort"] = "Product_Basic.Product_Sort";
            Relation["ProductInfo.Product_QuotaAmount"] = "Product_Basic.Product_QuotaAmount";
            Relation["ProductInfo.Product_IsListShow"] = "Product_Basic.Product_IsListShow";
            Relation["ProductInfo.Product_GroupCode"] = "Product_Basic.Product_GroupCode";
            Relation["ProductInfo.Product_Hits"] = "Product_Basic.Product_Hits";
            Relation["ProductInfo.Product_Site"] = "Product_Basic.Product_Site";
            Relation["ProductInfo.Product_SEO_Title"] = "Product_Basic.Product_SEO_Title";
            Relation["ProductInfo.Product_SEO_Keyword"] = "Product_Basic.Product_SEO_Keyword";
            Relation["ProductInfo.Product_SEO_Description"] = "Product_Basic.Product_SEO_Description";
            Relation["ProductInfo.Product_Trace_Code"] = "Product_Basic.Product_Trace_Code";
            Relation["ProductInfo.Product_PromotionTagID"] = "Product_Basic.Product_PromotionTagID";
            Relation["ProductInfo.Product_Service"] = "Product_Basic.Product_Service";
            Relation["LEFT(ProductInfo.Product_Code, 12)"] = "LEFT(Product_Basic.Product_Code, 12)";
            Relation["ProductInfo.Product_LastEditTime"] = "Product_Basic.Product_LastEditTime";
            Relation["ProductInfo.HeYue_Type"] = "Product_Basic.HeYue_Type";
            Relation["ProductInfo.Product_Extend_GroupID"] = "Product_Basic.Product_Extend_GroupID";
            Relation["ProductInfo.Product_Combination"] = "Product_Basic.Product_Combination";
            Relation["ProductInfo.Product_PrintImg"] = "Product_Basic.Product_PrintImg";
            Relation["ProductInfo.Product_IsAlone"] = "Product_Basic.Product_IsAlone";
            Relation["ProductInfo.Product_Keyword"] = "Product_Basic.Product_Keyword";
            //Product_Price
            Relation["ProductPriceInfo.Product_Price_ID"] = "Product_Price.Product_Price_ID";
            Relation["ProductPriceInfo.Product_Price_ProcutID"] = "Product_Price.Product_Price_ProcutID";
            Relation["ProductPriceInfo.Product_Price_MemberGradeID"] = "Product_Price.Product_Price_MemberGradeID";
            Relation["ProductPriceInfo.Product_Price_Price"] = "Product_Price.Product_Price_Price";

            //Product_Audit_Reason
            Relation["ProductAuditReasonInfo.Product_Audit_Reason_ID"] = "Product_Audit_Reason.Product_Audit_Reason_ID";
            Relation["ProductAuditReasonInfo.Product_Audit_Reason_Note"] = "Product_Audit_Reason.Product_Audit_Reason_Note";

            //Product_Extend
            Relation["ProductExtendInfo.Product_Extend_ID"] = "Product_Extend.Product_Extend_ID";
            Relation["ProductExtendInfo.Product_Extend_ProductID"] = "Product_Extend.Product_Extend_ProductID";
            Relation["ProductExtendInfo.Product_Extend_ExtendID"] = "Product_Extend.Product_Extend_ExtendID";
            Relation["ProductExtendInfo.Product_Extend_Value"] = "Product_Extend.Product_Extend_Value";
            Relation["ProductExtendInfo.Product_Extend_Img"] = "Product_Extend.Product_Extend_Img";

            //Product_HistoryPrice
            Relation["ProductHistoryPriceInfo.History_ID"] = "Product_HistoryPrice.History_ID";
            Relation["ProductHistoryPriceInfo.History_SysName"] = "Product_HistoryPrice.History_SysName";
            Relation["ProductHistoryPriceInfo.History_ProductID"] = "Product_HistoryPrice.History_ProductID";
            Relation["ProductHistoryPriceInfo.History_PriceType"] = "Product_HistoryPrice.History_PriceType";
            Relation["ProductHistoryPriceInfo.History_Price_Original"] = "Product_HistoryPrice.History_Price_Original";
            Relation["ProductHistoryPriceInfo.History_Price_New"] = "Product_HistoryPrice.History_Price_New";
            Relation["ProductHistoryPriceInfo.History_Addtime"] = "Product_HistoryPrice.History_Addtime";

            //Shopping_Ask
            Relation["ShoppingAskInfo.Ask_ID"] = "Shopping_Ask.Ask_ID";
            Relation["ShoppingAskInfo.Ask_Type"] = "Shopping_Ask.Ask_Type";
            Relation["ShoppingAskInfo.Ask_Content"] = "Shopping_Ask.Ask_Content";
            Relation["ShoppingAskInfo.Ask_Reply"] = "Shopping_Ask.Ask_Reply";
            Relation["ShoppingAskInfo.Ask_Addtime"] = "Shopping_Ask.Ask_Addtime";
            Relation["ShoppingAskInfo.Ask_MemberID"] = "Shopping_Ask.Ask_MemberID";
            Relation["ShoppingAskInfo.Ask_ProductID"] = "Shopping_Ask.Ask_ProductID";
            Relation["ShoppingAskInfo.Ask_Pleasurenum"] = "Shopping_Ask.Ask_Pleasurenum";
            Relation["ShoppingAskInfo.Ask_Displeasure"] = "Shopping_Ask.Ask_Displeasure";
            Relation["ShoppingAskInfo.Ask_Isreply"] = "Shopping_Ask.Ask_Isreply";
            Relation["ShoppingAskInfo.Ask_Site"] = "Shopping_Ask.Ask_Site";

            //Stockout_Booking
            Relation["StockoutBookingInfo.Stockout_ID"] = "Stockout_Booking.Stockout_ID";
            Relation["StockoutBookingInfo.Stockout_Product_Name"] = "Stockout_Booking.Stockout_Product_Name";
            Relation["StockoutBookingInfo.Stockout_Product_Describe"] = "Stockout_Booking.Stockout_Product_Describe";
            Relation["StockoutBookingInfo.Stockout_Member_Name"] = "Stockout_Booking.Stockout_Member_Name";
            Relation["StockoutBookingInfo.Stockout_Member_Tel"] = "Stockout_Booking.Stockout_Member_Tel";
            Relation["StockoutBookingInfo.Stockout_Member_Email"] = "Stockout_Booking.Stockout_Member_Email";
            Relation["StockoutBookingInfo.Stockout_IsRead"] = "Stockout_Booking.Stockout_IsRead";
            Relation["StockoutBookingInfo.Stockout_Addtime"] = "Stockout_Booking.Stockout_Addtime";
            Relation["StockoutBookingInfo.Stockout_Site"] = "Stockout_Booking.Stockout_Site";


            //RBAC_User
            Relation["RBACUserInfo.RBAC_User_ID"] = "RBAC_User.RBAC_User_ID";
            Relation["RBACUserInfo.RBAC_User_GroupID"] = "RBAC_User.RBAC_User_GroupID";
            Relation["RBACUserInfo.RBAC_User_Name"] = "RBAC_User.RBAC_User_Name";
            Relation["RBACUserInfo.RBAC_User_Password"] = "RBAC_User.RBAC_User_Password";
            Relation["RBACUserInfo.RBAC_User_LastLogin"] = "RBAC_User.RBAC_User_LastLogin";
            Relation["RBACUserInfo.RBAC_User_LastLoginIP"] = "RBAC_User.RBAC_User_LastLoginIP";
            Relation["RBACUserInfo.RBAC_User_Addtime"] = "RBAC_User.RBAC_User_Addtime";
            Relation["RBACUserInfo.RBAC_User_Site"] = "RBAC_User.RBAC_User_Site";

            //RBAC_UserGroup
            Relation["RBACUserGroupInfo.RBAC_UserGroup_ID"] = "RBAC_UserGroup.RBAC_UserGroup_ID";
            Relation["RBACUserGroupInfo.RBAC_UserGroup_Name"] = "RBAC_UserGroup.RBAC_UserGroup_Name";
            Relation["RBACUserGroupInfo.RBAC_UserGroup_ParentID"] = "RBAC_UserGroup.RBAC_UserGroup_ParentID";
            Relation["RBACUserGroupInfo.RBAC_UserGroup_Site"] = "RBAC_UserGroup.RBAC_UserGroup_Site";

            //Notice_Cate
            Relation["NoticeCateInfo.Notice_Cate_ID"] = "Notice_Cate.Notice_Cate_ID";
            Relation["NoticeCateInfo.Notice_Cate_Name"] = "Notice_Cate.Notice_Cate_Name";
            Relation["NoticeCateInfo.Notice_Cate_Sort"] = "Notice_Cate.Notice_Cate_Sort";
            Relation["NoticeCateInfo.Notice_Cate_Site"] = "Notice_Cate.Notice_Cate_Site";
            Relation["NoticeCateInfo.Notice_Cate_SEO_Title"] = "Notice_Cate.Notice_Cate_SEO_Title";
            Relation["NoticeCateInfo.Notice_Cate_SEO_Keyword"] = "Notice_Cate.Notice_Cate_SEO_Keyword";
            Relation["NoticeCateInfo.Notice_Cate_SEO_Description"] = "Notice_Cate.Notice_Cate_SEO_Description";

            //Notice
            Relation["NoticeInfo.Notice_ID"] = "Notice.Notice_ID";
            Relation["NoticeInfo.Notice_Cate"] = "Notice.Notice_Cate";
            Relation["NoticeInfo.Notice_IsHot"] = "Notice.Notice_IsHot";
            Relation["NoticeInfo.Notice_IsAudit"] = "Notice.Notice_IsAudit";
            Relation["NoticeInfo.Notice_SysUserID"] = "Notice.Notice_SysUserID";
            Relation["NoticeInfo.Notice_SellerID"] = "Notice.Notice_SellerID";
            Relation["NoticeInfo.Notice_Title"] = "Notice.Notice_Title";
            Relation["NoticeInfo.Notice_Content"] = "Notice.Notice_Content";
            Relation["NoticeInfo.Notice_Addtime"] = "Notice.Notice_Addtime";
            Relation["NoticeInfo.Notice_Site"] = "Notice.Notice_Site";
            Relation["NoticeInfo.Notice_SEO_Title"] = "Notice.Notice_SEO_Title";
            Relation["NoticeInfo.Notice_SEO_Keyword"] = "Notice.Notice_SEO_Keyword";
            Relation["NoticeInfo.Notice_SEO_Description"] = "Notice.Notice_SEO_Description";
            Relation["NoticeInfo.Notice_ShowTime"] = "Notice.Notice_ShowTime";

            //About
            Relation["AboutInfo.About_ID"] = "About.About_ID";
            Relation["AboutInfo.About_IsActive"] = "About.About_IsActive";
            Relation["AboutInfo.About_Title"] = "About.About_Title";
            Relation["AboutInfo.About_Sign"] = "About.About_Sign";
            Relation["AboutInfo.About_Content"] = "About.About_Content";
            Relation["AboutInfo.About_Sort"] = "About.About_Sort";
            Relation["AboutInfo.About_Site"] = "About.About_Site";
            Relation["AboutInfo.About_IsTop"] = "About.About_IsTop";
            Relation["AboutInfo.About_SEO_Title"] = "About.About_SEO_Title";
            Relation["AboutInfo.About_SEO_Keyword"] = "About.About_SEO_Keyword";
            Relation["AboutInfo.About_SEO_Description"] = "About.About_SEO_Description";

            //Help_Cate
            Relation["HelpCateInfo.Help_Cate_ID"] = "Help_Cate.Help_Cate_ID";
            Relation["HelpCateInfo.Help_Cate_ParentID"] = "Help_Cate.Help_Cate_ParentID";
            Relation["HelpCateInfo.Help_Cate_Name"] = "Help_Cate.Help_Cate_Name";
            Relation["HelpCateInfo.Help_Cate_Sort"] = "Help_Cate.Help_Cate_Sort";
            Relation["HelpCateInfo.Help_Cate_Site"] = "Help_Cate.Help_Cate_Site";
            Relation["HelpCateInfo.Help_Cate_SEO_Title"] = "Help_Cate.Help_Cate_SEO_Title";
            Relation["HelpCateInfo.Help_Cate_SEO_Keyword"] = "Help_Cate.Help_Cate_SEO_Keyword";
            Relation["HelpCateInfo.Help_Cate_SEO_Description"] = "Help_Cate.Help_Cate_SEO_Description";

            //Help
            Relation["HelpInfo.Help_ID"] = "Help.Help_ID";
            Relation["HelpInfo.Help_CateID"] = "Help.Help_CateID";
            Relation["HelpInfo.Help_IsFAQ"] = "Help.Help_IsFAQ";
            Relation["HelpInfo.Help_IsActive"] = "Help.Help_IsActive";
            Relation["HelpInfo.Help_Title"] = "Help.Help_Title";
            Relation["HelpInfo.Help_Content"] = "Help.Help_Content";
            Relation["HelpInfo.Help_Sort"] = "Help.Help_Sort";
            Relation["HelpInfo.Help_Site"] = "Help.Help_Site";
            Relation["HelpInfo.Help_SEO_Title"] = "Help.Help_SEO_Title";
            Relation["HelpInfo.Help_SEO_Keyword"] = "Help.Help_SEO_Keyword";
            Relation["HelpInfo.Help_SEO_Description"] = "Help.Help_SEO_Description";

            //AD_Position
            Relation["ADPositionInfo.Ad_Position_ID"] = "AD_Position.Ad_Position_ID";
            Relation["ADPositionInfo.Ad_Position_ChannelID"] = "AD_Position.Ad_Position_ChannelID";
            Relation["ADPositionInfo.Ad_Position_Name"] = "AD_Position.Ad_Position_Name";
            Relation["ADPositionInfo.Ad_Position_Value"] = "AD_Position.Ad_Position_Value";
            Relation["ADPositionInfo.Ad_Position_Width"] = "AD_Position.Ad_Position_Width";
            Relation["ADPositionInfo.Ad_Position_Height"] = "AD_Position.Ad_Position_Height";
            Relation["ADPositionInfo.Ad_Position_IsActive"] = "AD_Position.Ad_Position_IsActive";
            Relation["ADPositionInfo.Ad_Position_Site"] = "AD_Position.Ad_Position_Site";

            //AD
            Relation["ADInfo.Ad_ID"] = "AD.Ad_ID";
            Relation["ADInfo.Ad_Title"] = "AD.Ad_Title";
            Relation["ADInfo.Ad_Kind"] = "AD.Ad_Kind";
            Relation["ADInfo.Ad_MediaKind"] = "AD.Ad_MediaKind";
            Relation["ADInfo.Ad_Media"] = "AD.Ad_Media";
            Relation["ADInfo.Ad_Link"] = "AD.Ad_Link";
            Relation["ADInfo.Ad_Show_Freq"] = "AD.Ad_Show_Freq";
            Relation["ADInfo.Ad_Show_times"] = "AD.Ad_Show_times";
            Relation["ADInfo.Ad_Hits"] = "AD.Ad_Hits";
            Relation["ADInfo.Ad_StartDate"] = "AD.Ad_StartDate";
            Relation["ADInfo.Ad_EndDate"] = "AD.Ad_EndDate";
            Relation["ADInfo.Ad_IsContain"] = "AD.Ad_IsContain";
            Relation["ADInfo.Ad_Propertys"] = "AD.Ad_Propertys";
            Relation["ADInfo.Ad_Sort"] = "AD.Ad_Sort";
            Relation["ADInfo.Ad_IsActive"] = "AD.Ad_IsActive";
            Relation["ADInfo.Ad_Site"] = "AD.Ad_Site";

            //AD_Position_Channel
            Relation["ADPositionChannelInfo.AD_Position_Channel_ID"] = "AD_Position_Channel.AD_Position_Channel_ID";
            Relation["ADPositionChannelInfo.AD_Position_Channel_Name"] = "AD_Position_Channel.AD_Position_Channel_Name";
            Relation["ADPositionChannelInfo.AD_Position_Channel_Note"] = "AD_Position_Channel.AD_Position_Channel_Note";
            Relation["ADPositionChannelInfo.AD_Position_Channel_Site"] = "AD_Position_Channel.AD_Position_Channel_Site";

            //FriendlyLink_Cate
            Relation["FriendlyLinkCateInfo.FriendlyLink_Cate_ID"] = "FriendlyLink_Cate.FriendlyLink_Cate_ID";
            Relation["FriendlyLinkCateInfo.FriendlyLink_Cate_Name"] = "FriendlyLink_Cate.FriendlyLink_Cate_Name";
            Relation["FriendlyLinkCateInfo.FriendlyLink_Cate_Sort"] = "FriendlyLink_Cate.FriendlyLink_Cate_Sort";
            Relation["FriendlyLinkCateInfo.FriendlyLink_Cate_Site"] = "FriendlyLink_Cate.FriendlyLink_Cate_Site";
            Relation["FriendlyLinkCateInfo.FriendlyLink_Cate_SEO_Title"] = "FriendlyLink_Cate.FriendlyLink_Cate_SEO_Title";
            Relation["FriendlyLinkCateInfo.FriendlyLink_Cate_SEO_Keyword"] = "FriendlyLink_Cate.FriendlyLink_Cate_SEO_Keyword";
            Relation["FriendlyLinkCateInfo.FriendlyLink_Cate_SEO_Description"] = "FriendlyLink_Cate.FriendlyLink_Cate_SEO_Description";

            //FriendlyLink
            Relation["FriendlyLinkInfo.FriendlyLink_ID"] = "FriendlyLink.FriendlyLink_ID";
            Relation["FriendlyLinkInfo.FriendlyLink_CateID"] = "FriendlyLink.FriendlyLink_CateID";
            Relation["FriendlyLinkInfo.FriendlyLink_Name"] = "FriendlyLink.FriendlyLink_Name";
            Relation["FriendlyLinkInfo.FriendlyLink_Img"] = "FriendlyLink.FriendlyLink_Img";
            Relation["FriendlyLinkInfo.FriendlyLink_URL"] = "FriendlyLink.FriendlyLink_URL";
            Relation["FriendlyLinkInfo.FriendlyLink_IsActive"] = "FriendlyLink.FriendlyLink_IsActive";
            Relation["FriendlyLinkInfo.FriendlyLink_IsImg"] = "FriendlyLink.FriendlyLink_IsImg";
            Relation["FriendlyLinkInfo.FriendlyLink_Site"] = "FriendlyLink.FriendlyLink_Site";
            Relation["FriendlyLinkInfo.FriendlyLink_Sort"] = "FriendlyLink.FriendlyLink_Sort";

            //RBAC_ResourceGroup
            Relation["RBACResourceGroupInfo.RBAC_ResourceGroup_ID"] = "RBAC_ResourceGroup.RBAC_ResourceGroup_ID";
            Relation["RBACResourceGroupInfo.RBAC_ResourceGroup_Name"] = "RBAC_ResourceGroup.RBAC_ResourceGroup_Name";
            Relation["RBACResourceGroupInfo.RBAC_ResourceGroup_ParentID"] = "RBAC_ResourceGroup.RBAC_ResourceGroup_ParentID";
            Relation["RBACResourceGroupInfo.RBAC_ResourceGroup_Site"] = "RBAC_ResourceGroup.RBAC_ResourceGroup_Site";

            //RBAC_Resource
            Relation["RBACResourceInfo.RBAC_Resource_ID"] = "RBAC_Resource.RBAC_Resource_ID";
            Relation["RBACResourceInfo.RBAC_Resource_GroupID"] = "RBAC_Resource.RBAC_Resource_GroupID";
            Relation["RBACResourceInfo.RBAC_Resource_Name"] = "RBAC_Resource.RBAC_Resource_Name";
            Relation["RBACResourceInfo.RBAC_Resource_Site"] = "RBAC_Resource.RBAC_Resource_Site";

            //RBAC_Privilege
            Relation["RBACPrivilegeInfo.RBAC_Privilege_ID"] = "RBAC_Privilege.RBAC_Privilege_ID";
            Relation["RBACPrivilegeInfo.RBAC_Privilege_ResourceID"] = "RBAC_Privilege.RBAC_Privilege_ResourceID";
            Relation["RBACPrivilegeInfo.RBAC_Privilege_Name"] = "RBAC_Privilege.RBAC_Privilege_Name";
            Relation["RBACPrivilegeInfo.RBAC_Privilege_IsActive"] = "RBAC_Privilege.RBAC_Privilege_IsActive";
            Relation["RBACPrivilegeInfo.RBAC_Privilege_Addtime"] = "RBAC_Privilege.RBAC_Privilege_Addtime";

            //RBAC_Role
            Relation["RBACRoleInfo.RBAC_Role_ID"] = "RBAC_Role.RBAC_Role_ID";
            Relation["RBACRoleInfo.RBAC_Role_Name"] = "RBAC_Role.RBAC_Role_Name";
            Relation["RBACRoleInfo.RBAC_Role_Description"] = "RBAC_Role.RBAC_Role_Description";
            Relation["RBACRoleInfo.RBAC_Role_IsSystem"] = "RBAC_Role.RBAC_Role_IsSystem";
            Relation["RBACRoleInfo.RBAC_Role_Site"] = "RBAC_Role.RBAC_Role_Site";

            //Member
            //Member
            Relation["MemberInfo.Member_ID"] = "Member.Member_ID";
            Relation["MemberInfo.Member_Email"] = "Member.Member_Email";
            Relation["MemberInfo.Member_Emailverify"] = "Member.Member_Emailverify";
            Relation["MemberInfo.Member_LoginMobile"] = "Member.Member_LoginMobile";
            Relation["MemberInfo.Member_LoginMobileverify"] = "Member.Member_LoginMobileverify";
            Relation["MemberInfo.Member_NickName"] = "Member.Member_NickName";
            Relation["MemberInfo.Member_Password"] = "Member.Member_Password";
            Relation["MemberInfo.Member_VerifyCode"] = "Member.Member_VerifyCode";
            Relation["MemberInfo.Member_LoginCount"] = "Member.Member_LoginCount";
            Relation["MemberInfo.Member_LastLogin_IP"] = "Member.Member_LastLogin_IP";
            Relation["MemberInfo.Member_LastLogin_Time"] = "Member.Member_LastLogin_Time";
            Relation["MemberInfo.Member_CoinCount"] = "Member.Member_CoinCount";
            Relation["MemberInfo.Member_CoinRemain"] = "Member.Member_CoinRemain";
            Relation["MemberInfo.Member_Addtime"] = "Member.Member_Addtime";
            Relation["MemberInfo.Member_Trash"] = "Member.Member_Trash";
            Relation["MemberInfo.Member_Grade"] = "Member.Member_Grade";
            Relation["MemberInfo.Member_Account"] = "Member.Member_Account";
            Relation["MemberInfo.Member_Frozen"] = "Member.Member_Frozen";
            Relation["MemberInfo.Member_AllowSysEmail"] = "Member.Member_AllowSysEmail";
            Relation["MemberInfo.Member_AllowSysMobile"] = "Member.Member_AllowSysMobile";
            Relation["MemberInfo.Member_Site"] = "Member.Member_Site";
            Relation["MemberInfo.Member_Source"] = "Member.Member_Source";
            Relation["MemberInfo.U_Member_QQ"] = "Member.U_Member_QQ";
            Relation["MemberInfo.U_Member_MSN"] = "Member.U_Member_MSN";
            Relation["MemberInfo.U_Member_Question"] = "Member.U_Member_Question";
            Relation["MemberInfo.U_Member_Answer"] = "Member.U_Member_Answer";
            Relation["MemberInfo.U_Member_Male"] = "Member.U_Member_Male";
            Relation["MemberInfo.U_MeMber_Birth"] = "Member.U_MeMber_Birth";
            Relation["MemberInfo.U_Member_Bloodtype"] = "Member.U_Member_Bloodtype";
            Relation["MemberInfo.U_Member_Realname"] = "Member.U_Member_Realname";
            Relation["MemberInfo.U_Member_Country"] = "Member.U_Member_Country";
            Relation["MemberInfo.U_Member_Province"] = "Member.U_Member_Province";
            Relation["MemberInfo.U_Member_City"] = "Member.U_Member_City";
            Relation["MemberInfo.U_Member_Address"] = "Member.U_Member_Address";
            Relation["MemberInfo.U_Member_Job"] = "Member.U_Member_Job";
            Relation["MemberInfo.U_Member_Edu"] = "Member.U_Member_Edu";
            Relation["MemberInfo.U_Member_Postcode"] = "Member.U_Member_Postcode";
            Relation["MemberInfo.U_Member_School"] = "Member.U_Member_School";
            Relation["MemberInfo.U_Member_IDCard"] = "Member.U_Member_IDCard";
            Relation["MemberInfo.U_Member_Mark"] = "Member.U_Member_Mark";
            Relation["MemberInfo.U_Member_Article_Commend"] = "Member.U_Member_Article_Commend";
            Relation["MemberInfo.U_Member_State"] = "Member.U_Member_State";
            Relation["MemberInfo.U_Member_OpenID"] = "Member.U_Member_OpenID";


            //Member_Log
            Relation["MemberLogInfo.Log_ID"] = "Member_Log.Log_ID";
            Relation["MemberLogInfo.Log_Member_ID"] = "Member_Log.Log_Member_ID";
            Relation["MemberLogInfo.Log_Member_Name"] = "Member_Log.Log_Member_Name";
            Relation["MemberLogInfo.Log_Member_Result"] = "Member_Log.Log_Member_Result";
            Relation["MemberLogInfo.Log_Member_Action"] = "Member_Log.Log_Member_Action";
            Relation["MemberLogInfo.Log_Addtime"] = "Member_Log.Log_Addtime";


            //Member_Grade
            Relation["MemberGradeInfo.Member_Grade_ID"] = "Member_Grade.Member_Grade_ID";
            Relation["MemberGradeInfo.Member_Grade_Name"] = "Member_Grade.Member_Grade_Name";
            Relation["MemberGradeInfo.Member_Grade_Percent"] = "Member_Grade.Member_Grade_Percent";
            Relation["MemberGradeInfo.Member_Grade_Default"] = "Member_Grade.Member_Grade_Default";
            Relation["MemberGradeInfo.Member_Grade_RequiredCoin"] = "Member_Grade.Member_Grade_RequiredCoin";
            Relation["MemberGradeInfo.Member_Grade_CoinRate"] = "Member_Grade.Member_Grade_CoinRate";
            Relation["MemberGradeInfo.Member_Grade_Addtime"] = "Member_Grade.Member_Grade_Addtime";
            Relation["MemberGradeInfo.Member_Grade_Site"] = "Member_Grade.Member_Grade_Site";

            //Member_Consumption


            Relation["MemberConsumptionInfo.Consump_ID"] = "Member_Consumption.Consump_ID";
            Relation["MemberConsumptionInfo.Consump_MemberID"] = "Member_Consumption.Consump_MemberID";
            Relation["MemberConsumptionInfo.Consump_CoinRemain"] = "Member_Consumption.Consump_CoinRemain";
            Relation["MemberConsumptionInfo.Consump_Coin"] = "Member_Consumption.Consump_Coin";
            Relation["MemberConsumptionInfo.Consump_Reason"] = "Member_Consumption.Consump_Reason";
            Relation["MemberConsumptionInfo.Consump_Addtime"] = "Member_Consumption.Consump_Addtime";
            Relation["MemberConsumptionInfo.Consump_Qid"] = "Member_Consumption.Consump_Qid";


            //Member_Favorites
            Relation["MemberFavoritesInfo.Member_Favorites_ID"] = "Member_Favorites.Member_Favorites_ID";
            Relation["MemberFavoritesInfo.Member_Favorites_MemberID"] = "Member_Favorites.Member_Favorites_MemberID";
            Relation["MemberFavoritesInfo.Member_Favorites_Type"] = "Member_Favorites.Member_Favorites_Type";
            Relation["MemberFavoritesInfo.Member_Favorites_TargetID"] = "Member_Favorites.Member_Favorites_TargetID";
            Relation["MemberFavoritesInfo.Member_Favorites_Addtime"] = "Member_Favorites.Member_Favorites_Addtime";
            Relation["MemberFavoritesInfo.Member_Favorites_Site"] = "Member_Favorites.Member_Favorites_Site";

            //Feedback
            Relation["FeedBackInfo.Feedback_ID"] = "Feedback.Feedback_ID";
            Relation["FeedBackInfo.Feedback_Type"] = "Feedback.Feedback_Type";
            Relation["FeedBackInfo.Feedback_MemberID"] = "Feedback.Feedback_MemberID";
            Relation["FeedBackInfo.Feedback_Name"] = "Feedback.Feedback_Name";
            Relation["FeedBackInfo.Feedback_Tel"] = "Feedback.Feedback_Tel";
            Relation["FeedBackInfo.Feedback_Email"] = "Feedback.Feedback_Email";
            Relation["FeedBackInfo.Feedback_Content"] = "Feedback.Feedback_Content";
            Relation["FeedBackInfo.Feedback_Addtime"] = "Feedback.Feedback_Addtime";
            Relation["FeedBackInfo.Feedback_IsRead"] = "Feedback.Feedback_IsRead";
            Relation["FeedBackInfo.Feedback_Reply_IsRead"] = "Feedback.Feedback_Reply_IsRead";
            Relation["FeedBackInfo.Feedback_Reply_Content"] = "Feedback.Feedback_Reply_Content";
            Relation["FeedBackInfo.Feedback_Reply_Addtime"] = "Feedback.Feedback_Reply_Addtime";
            Relation["FeedBackInfo.Feedback_Site"] = "Feedback.Feedback_Site";

            //Member_Address
            Relation["MemberAddressInfo.Member_Address_ID"] = "Member_Address.Member_Address_ID";
            Relation["MemberAddressInfo.Member_Address_MemberID"] = "Member_Address.Member_Address_MemberID";
            Relation["MemberAddressInfo.Member_Address_Country"] = "Member_Address.Member_Address_Country";
            Relation["MemberAddressInfo.Member_Address_State"] = "Member_Address.Member_Address_State";
            Relation["MemberAddressInfo.Member_Address_City"] = "Member_Address.Member_Address_City";
            Relation["MemberAddressInfo.Member_Address_County"] = "Member_Address.Member_Address_County";
            Relation["MemberAddressInfo.Member_Address_StreetAddress"] = "Member_Address.Member_Address_StreetAddress";
            Relation["MemberAddressInfo.Member_Address_Zip"] = "Member_Address.Member_Address_Zip";
            Relation["MemberAddressInfo.Member_Address_Name"] = "Member_Address.Member_Address_Name";
            Relation["MemberAddressInfo.Member_Address_Phone_Countrycode"] = "Member_Address.Member_Address_Phone_Countrycode";
            Relation["MemberAddressInfo.Member_Address_Phone_Areacode"] = "Member_Address.Member_Address_Phone_Areacode";
            Relation["MemberAddressInfo.Member_Address_Phone_Number"] = "Member_Address.Member_Address_Phone_Number";
            Relation["MemberAddressInfo.Member_Address_Mobile"] = "Member_Address.Member_Address_Mobile";
            Relation["MemberAddressInfo.Member_Address_Site"] = "Member_Address.Member_Address_Site";

            //Pay_Way
            Relation["PayWayInfo.Pay_Way_ID"] = "Pay_Way.Pay_Way_ID";
            Relation["PayWayInfo.Pay_Way_Type"] = "Pay_Way.Pay_Way_Type";
            Relation["PayWayInfo.Pay_Way_Name"] = "Pay_Way.Pay_Way_Name";
            Relation["PayWayInfo.Pay_Way_Sort"] = "Pay_Way.Pay_Way_Sort";
            Relation["PayWayInfo.Pay_Way_Status"] = "Pay_Way.Pay_Way_Status";
            Relation["PayWayInfo.Pay_Way_Cod"] = "Pay_Way.Pay_Way_Cod";
            Relation["PayWayInfo.Pay_Way_Img"] = "Pay_Way.Pay_Way_Img";
            Relation["PayWayInfo.Pay_Way_Note"] = "Pay_Way.Pay_Way_Note";
            Relation["PayWayInfo.Pay_Way_Intro"] = "Pay_Way.Pay_Way_Intro";
            Relation["PayWayInfo.Pay_Way_Site"] = "Pay_Way.Pay_Way_Site";

            //Delivery_Way
            Relation["DeliveryWayInfo.Delivery_Way_ID"] = "Delivery_Way.Delivery_Way_ID";
            Relation["DeliveryWayInfo.Delivery_Way_Name"] = "Delivery_Way.Delivery_Way_Name";
            Relation["DeliveryWayInfo.Delivery_Way_Sort"] = "Delivery_Way.Delivery_Way_Sort";
            Relation["DeliveryWayInfo.Delivery_Way_InitialWeight"] = "Delivery_Way.Delivery_Way_InitialWeight";
            Relation["DeliveryWayInfo.Delivery_Way_UpWeight"] = "Delivery_Way.Delivery_Way_UpWeight";
            Relation["DeliveryWayInfo.Delivery_Way_FeeType"] = "Delivery_Way.Delivery_Way_FeeType";
            Relation["DeliveryWayInfo.Delivery_Way_Fee"] = "Delivery_Way.Delivery_Way_Fee";
            Relation["DeliveryWayInfo.Delivery_Way_InitialFee"] = "Delivery_Way.Delivery_Way_InitialFee";
            Relation["DeliveryWayInfo.Delivery_Way_UpFee"] = "Delivery_Way.Delivery_Way_UpFee";
            Relation["DeliveryWayInfo.Delivery_Way_Status"] = "Delivery_Way.Delivery_Way_Status";
            Relation["DeliveryWayInfo.Delivery_Way_Cod"] = "Delivery_Way.Delivery_Way_Cod";
            Relation["DeliveryWayInfo.Delivery_Way_Img"] = "Delivery_Way.Delivery_Way_Img";
            Relation["DeliveryWayInfo.Delivery_Way_Intro"] = "Delivery_Way.Delivery_Way_Intro";
            Relation["DeliveryWayInfo.Delivery_Way_Site"] = "Delivery_Way.Delivery_Way_Site";

            //Delivery_Way_District
            Relation["DeliveryWayDistrictInfo.District_ID"] = "Delivery_Way_District.District_ID";
            Relation["DeliveryWayDistrictInfo.District_DeliveryWayID"] = "Delivery_Way_District.District_DeliveryWayID";
            Relation["DeliveryWayDistrictInfo.District_Country"] = "Delivery_Way_District.District_Country";
            Relation["DeliveryWayDistrictInfo.District_State"] = "Delivery_Way_District.District_State";
            Relation["DeliveryWayDistrictInfo.District_City"] = "Delivery_Way_District.District_City";
            Relation["DeliveryWayDistrictInfo.District_County"] = "Delivery_Way_District.District_County";

            //Delivery_Time
            Relation["DeliveryTimeInfo.Delivery_Time_ID"] = "Delivery_Time.Delivery_Time_ID";
            Relation["DeliveryTimeInfo.Delivery_Time_Name"] = "Delivery_Time.Delivery_Time_Name";
            Relation["DeliveryTimeInfo.Delivery_Time_Sort"] = "Delivery_Time.Delivery_Time_Sort";
            Relation["DeliveryTimeInfo.Delivery_Time_IsActive"] = "Delivery_Time.Delivery_Time_IsActive";
            Relation["DeliveryTimeInfo.Delivery_Time_Site"] = "Delivery_Time.Delivery_Time_Site";


            //Orders_Goods_tmp
            Relation["OrdersGoodsTmpInfo.Orders_Goods_ID"] = "Orders_Goods_tmp.Orders_Goods_ID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Type"] = "Orders_Goods_tmp.Orders_Goods_Type";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_BuyerID"] = "Orders_Goods_tmp.Orders_Goods_BuyerID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_SessionID"] = "Orders_Goods_tmp.Orders_Goods_SessionID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_ParentID"] = "Orders_Goods_tmp.Orders_Goods_ParentID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_ID"] = "Orders_Goods_tmp.Orders_Goods_Product_ID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID"] = "Orders_Goods_tmp.Orders_Goods_Product_SupplierID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Code"] = "Orders_Goods_tmp.Orders_Goods_Product_Code";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_CateID"] = "Orders_Goods_tmp.Orders_Goods_Product_CateID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_BrandID"] = "Orders_Goods_tmp.Orders_Goods_Product_BrandID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Name"] = "Orders_Goods_tmp.Orders_Goods_Product_Name";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Img"] = "Orders_Goods_tmp.Orders_Goods_Product_Img";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Price"] = "Orders_Goods_tmp.Orders_Goods_Product_Price";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_MKTPrice"] = "Orders_Goods_tmp.Orders_Goods_Product_MKTPrice";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Maker"] = "Orders_Goods_tmp.Orders_Goods_Product_Maker";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Spec"] = "Orders_Goods_tmp.Orders_Goods_Product_Spec";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_AuthorizeCode"] = "Orders_Goods_tmp.Orders_Goods_Product_AuthorizeCode";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_brokerage"] = "Orders_Goods_tmp.Orders_Goods_Product_brokerage";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_SalePrice"] = "Orders_Goods_tmp.Orders_Goods_Product_SalePrice";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_PurchasingPrice"] = "Orders_Goods_tmp.Orders_Goods_Product_PurchasingPrice";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Coin"] = "Orders_Goods_tmp.Orders_Goods_Product_Coin";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_IsFavor"] = "Orders_Goods_tmp.Orders_Goods_Product_IsFavor";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_UseCoin"] = "Orders_Goods_tmp.Orders_Goods_Product_UseCoin";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Amount"] = "Orders_Goods_tmp.Orders_Goods_Amount";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Addtime"] = "Orders_Goods_tmp.Orders_Goods_Addtime";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_OrdersID"] = "Orders_Goods_tmp.Orders_Goods_OrdersID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_CustomerID"] = "Orders_Goods_tmp.Orders_Goods_CustomerID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_AuthorizationCode"] = "Orders_Goods_tmp.Orders_Goods_AuthorizationCode";

            //Orders
            Relation["OrdersInfo.Orders_ID"] = "Orders.Orders_ID";
            Relation["OrdersInfo.Orders_SN"] = "Orders.Orders_SN";
            Relation["OrdersInfo.Orders_BuyerID"] = "Orders.Orders_BuyerID";
            Relation["OrdersInfo.Orders_SysUserID"] = "Orders.Orders_SysUserID";
            Relation["OrdersInfo.Orders_Status"] = "Orders.Orders_Status";
            Relation["OrdersInfo.Orders_ERPSyncStatus"] = "Orders.Orders_ERPSyncStatus";
            Relation["OrdersInfo.Orders_PaymentStatus"] = "Orders.Orders_PaymentStatus";
            Relation["OrdersInfo.Orders_PaymentStatus_Time"] = "Orders.Orders_PaymentStatus_Time";
            Relation["OrdersInfo.Orders_DeliveryStatus"] = "Orders.Orders_DeliveryStatus";
            Relation["OrdersInfo.Orders_DeliveryStatus_Time"] = "Orders.Orders_DeliveryStatus_Time";
            Relation["OrdersInfo.Orders_InvoiceStatus"] = "Orders.Orders_InvoiceStatus";
            Relation["OrdersInfo.Orders_Fail_SysUserID"] = "Orders.Orders_Fail_SysUserID";
            Relation["OrdersInfo.Orders_Fail_Note"] = "Orders.Orders_Fail_Note";
            Relation["OrdersInfo.Orders_Fail_Addtime"] = "Orders.Orders_Fail_Addtime";
            Relation["OrdersInfo.Orders_IsReturnCoin"] = "Orders.Orders_IsReturnCoin";
            Relation["OrdersInfo.Orders_Total_MKTPrice"] = "Orders.Orders_Total_MKTPrice";
            Relation["OrdersInfo.Orders_Total_Price"] = "Orders.Orders_Total_Price";
            Relation["OrdersInfo.Orders_Total_Freight"] = "Orders.Orders_Total_Freight";
            Relation["OrdersInfo.Orders_Total_Coin"] = "Orders.Orders_Total_Coin";
            Relation["OrdersInfo.Orders_Total_UseCoin"] = "Orders.Orders_Total_UseCoin";
            Relation["OrdersInfo.Orders_Total_PriceDiscount"] = "Orders.Orders_Total_PriceDiscount";
            Relation["OrdersInfo.Orders_Total_FreightDiscount"] = "Orders.Orders_Total_FreightDiscount";
            Relation["OrdersInfo.Orders_Total_PriceDiscount_Note"] = "Orders.Orders_Total_PriceDiscount_Note";
            Relation["OrdersInfo.Orders_Total_FreightDiscount_Note"] = "Orders.Orders_Total_FreightDiscount_Note";
            Relation["OrdersInfo.Orders_Total_AllPrice"] = "Orders.Orders_Total_AllPrice";
            Relation["OrdersInfo.Orders_Address_ID"] = "Orders.Orders_Address_ID";
            Relation["OrdersInfo.Orders_Address_Country"] = "Orders.Orders_Address_Country";
            Relation["OrdersInfo.Orders_Address_State"] = "Orders.Orders_Address_State";
            Relation["OrdersInfo.Orders_Address_City"] = "Orders.Orders_Address_City";
            Relation["OrdersInfo.Orders_Address_County"] = "Orders.Orders_Address_County";
            Relation["OrdersInfo.Orders_Address_StreetAddress"] = "Orders.Orders_Address_StreetAddress";
            Relation["OrdersInfo.Orders_Address_Zip"] = "Orders.Orders_Address_Zip";
            Relation["OrdersInfo.Orders_Address_Name"] = "Orders.Orders_Address_Name";
            Relation["OrdersInfo.Orders_Address_Phone_Countrycode"] = "Orders.Orders_Address_Phone_Countrycode";
            Relation["OrdersInfo.Orders_Address_Phone_Areacode"] = "Orders.Orders_Address_Phone_Areacode";
            Relation["OrdersInfo.Orders_Address_Phone_Number"] = "Orders.Orders_Address_Phone_Number";
            Relation["OrdersInfo.Orders_Address_Mobile"] = "Orders.Orders_Address_Mobile";
            Relation["OrdersInfo.Orders_Delivery_Time_ID"] = "Orders.Orders_Delivery_Time_ID";
            Relation["OrdersInfo.Orders_Delivery"] = "Orders.Orders_Delivery";
            Relation["OrdersInfo.Orders_Delivery_Name"] = "Orders.Orders_Delivery_Name";
            Relation["OrdersInfo.Orders_Payway"] = "Orders.Orders_Payway";
            Relation["OrdersInfo.Orders_Payway_Name"] = "Orders.Orders_Payway_Name";
            Relation["OrdersInfo.Orders_Note"] = "Orders.Orders_Note";
            Relation["OrdersInfo.Orders_Admin_Note"] = "Orders.Orders_Admin_Note";
            Relation["OrdersInfo.Orders_Admin_Sign"] = "Orders.Orders_Admin_Sign";
            Relation["OrdersInfo.Orders_Site"] = "Orders.Orders_Site";
            Relation["OrdersInfo.Orders_SourceType"] = "Orders.Orders_SourceType";
            Relation["OrdersInfo.Orders_Source"] = "Orders.Orders_Source";
            Relation["OrdersInfo.Orders_VerifyCode"] = "Orders.Orders_VerifyCode";
            Relation["OrdersInfo.U_Orders_IsMonitor"] = "Orders.U_Orders_IsMonitor";
            Relation["OrdersInfo.Orders_Addtime"] = "Orders.Orders_Addtime";
            Relation["OrdersInfo.Orders_From"] = "Orders.Orders_From";
            Relation["OrdersInfo.Orders_Account_Pay"] = "Orders.Orders_Account_Pay";
            Relation["OrdersInfo.Orders_Card_Pay"] = "Orders.Orders_Card_Pay";
            Relation["OrdersInfo.Orders_Total_RemainPrice"] = "Orders.Orders_Total_RemainPrice";
            Relation["OrdersInfo.Orders_IsEvaluate"] = "Orders.Orders_IsEvaluate";
            Relation["OrdersInfo.U_Orders_SMS_Status"] = "Orders.U_Orders_SMS_Status";
            Relation["OrdersInfo.U_Orders_IsPush"] = "Orders.U_Orders_IsPush";
            Relation["OrdersInfo.U_Salesclerk_Code"] = "Orders.U_Salesclerk_Code";
            Relation["OrdersInfo.U_Salesclerk_State"] = "Orders.U_Salesclerk_State";
            Relation["OrdersInfo.U_Salesclerk_ShopName"] = "Orders.U_Salesclerk_ShopName";
            Relation["OrdersInfo.U_Salesclerk_ClerkName"] = "Orders.U_Salesclerk_ClerkName";
            Relation["OrdersInfo.IsSettlement"] = "Orders.IsSettlement";
            Relation["OrdersInfo.Orders_Recovery"] = "Orders.Orders_Recovery";
            Relation["OrdersInfo.Orders_InvoiceEmail"] = "Orders.Orders_InvoiceEmail";

            //Orders_Delivery
            Relation["OrdersDeliveryInfo.Orders_Delivery_ID"] = "Orders_Delivery.Orders_Delivery_ID";
            Relation["OrdersDeliveryInfo.Orders_Delivery_OrdersID"] = "Orders_Delivery.Orders_Delivery_OrdersID";
            Relation["OrdersDeliveryInfo.Orders_Delivery_DeliveryStatus"] = "Orders_Delivery.Orders_Delivery_DeliveryStatus";
            Relation["OrdersDeliveryInfo.Orders_Delivery_SysUserID"] = "Orders_Delivery.Orders_Delivery_SysUserID";
            Relation["OrdersDeliveryInfo.Orders_Delivery_DocNo"] = "Orders_Delivery.Orders_Delivery_DocNo";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Name"] = "Orders_Delivery.Orders_Delivery_Name";
            Relation["OrdersDeliveryInfo.Orders_Delivery_companyName"] = "Orders_Delivery.Orders_Delivery_companyName";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Code"] = "Orders_Delivery.Orders_Delivery_Code";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Amount"] = "Orders_Delivery.Orders_Delivery_Amount";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Note"] = "Orders_Delivery.Orders_Delivery_Note";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Addtime"] = "Orders_Delivery.Orders_Delivery_Addtime";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Site"] = "Orders_Delivery.Orders_Delivery_Site";

            //Orders_Payment
            Relation["OrdersPaymentInfo.Orders_Payment_ID"] = "Orders_Payment.Orders_Payment_ID";
            Relation["OrdersPaymentInfo.Orders_Payment_OrdersID"] = "Orders_Payment.Orders_Payment_OrdersID";
            Relation["OrdersPaymentInfo.Orders_Payment_PaymentStatus"] = "Orders_Payment.Orders_Payment_PaymentStatus";
            Relation["OrdersPaymentInfo.Orders_Payment_SysUserID"] = "Orders_Payment.Orders_Payment_SysUserID";
            Relation["OrdersPaymentInfo.Orders_Payment_DocNo"] = "Orders_Payment.Orders_Payment_DocNo";
            Relation["OrdersPaymentInfo.Orders_Payment_Name"] = "Orders_Payment.Orders_Payment_Name";
            Relation["OrdersPaymentInfo.Orders_Payment_Amount"] = "Orders_Payment.Orders_Payment_Amount";
            Relation["OrdersPaymentInfo.Orders_Payment_Note"] = "Orders_Payment.Orders_Payment_Note";
            Relation["OrdersPaymentInfo.Orders_Payment_Addtime"] = "Orders_Payment.Orders_Payment_Addtime";
            Relation["OrdersPaymentInfo.Orders_Payment_Site"] = "Orders_Payment.Orders_Payment_Site";

            //Package
            Relation["PackageInfo.Package_ID"] = "Package.Package_ID";
            Relation["PackageInfo.Package_Name"] = "Package.Package_Name";
            Relation["PackageInfo.Package_IsInsale"] = "Package.Package_IsInsale";
            Relation["PackageInfo.Package_StockAmount"] = "Package.Package_StockAmount";
            Relation["PackageInfo.Package_Weight"] = "Package.Package_Weight";
            Relation["PackageInfo.Package_Price"] = "Package.Package_Price";
            Relation["PackageInfo.Package_Sort"] = "Package.Package_Sort";
            Relation["PackageInfo.Package_Addtime"] = "Package.Package_Addtime";
            Relation["PackageInfo.Package_Site"] = "Package.Package_Site";


            //Favor_Fee
            //Promotion_Favor_Fee
            Relation["PromotionFavorFeeInfo.Promotion_Fee_ID"] = "Promotion_Favor_Fee.Promotion_Fee_ID";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Title"] = "Promotion_Favor_Fee.Promotion_Fee_Title";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Target"] = "Promotion_Favor_Fee.Promotion_Fee_Target";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Payline"] = "Promotion_Favor_Fee.Promotion_Fee_Payline";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Manner"] = "Promotion_Favor_Fee.Promotion_Fee_Manner";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Price"] = "Promotion_Favor_Fee.Promotion_Fee_Price";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Starttime"] = "Promotion_Favor_Fee.Promotion_Fee_Starttime";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Endtime"] = "Promotion_Favor_Fee.Promotion_Fee_Endtime";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Sort"] = "Promotion_Favor_Fee.Promotion_Fee_Sort";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_IsActive"] = "Promotion_Favor_Fee.Promotion_Fee_IsActive";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_IsChecked"] = "Promotion_Favor_Fee.Promotion_Fee_IsChecked";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Note"] = "Promotion_Favor_Fee.Promotion_Fee_Note";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Addtime"] = "Promotion_Favor_Fee.Promotion_Fee_Addtime";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Site"] = "Promotion_Favor_Fee.Promotion_Fee_Site";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_IsFirst"] = "Promotion_Favor_Fee.Promotion_Fee_IsFirst";

            //Promotion
            Relation["PromotionInfo.Promotion_ID"] = "Promotion.Promotion_ID";
            Relation["PromotionInfo.Promotion_Title"] = "Promotion.Promotion_Title";
            Relation["PromotionInfo.Promotion_Type"] = "Promotion.Promotion_Type";
            Relation["PromotionInfo.Promotion_TopHtml"] = "Promotion.Promotion_TopHtml";
            Relation["PromotionInfo.Promotion_Addtime"] = "Promotion.Promotion_Addtime";
            Relation["PromotionInfo.Promotion_Site"] = "Promotion.Promotion_Site";
            Relation["PromotionInfo.Promotion_IsTop"] = "Promotion.Promotion_IsTop";

            //Promotion_Group
            Relation["PromotionGroupInfo.Promotion_Group_ID"] = "Promotion_Group.Promotion_Group_ID";
            Relation["PromotionGroupInfo.Promotion_Group_Title"] = "Promotion_Group.Promotion_Group_Title";
            Relation["PromotionGroupInfo.Promotion_Group_PromotionID"] = "Promotion_Group.Promotion_Group_PromotionID";
            Relation["PromotionGroupInfo.Promotion_Group_Addtime"] = "Promotion_Group.Promotion_Group_Addtime";
            Relation["PromotionGroupInfo.Promotion_Group_Site"] = "Promotion_Group.Promotion_Group_Site";

            //Promotion_Limit_Group
            Relation["PromotionLimitGroupInfo.Promotion_Limit_Group_ID"] = "Promotion_Limit_Group.Promotion_Limit_Group_ID";
            Relation["PromotionLimitGroupInfo.Promotion_Limit_Group_Name"] = "Promotion_Limit_Group.Promotion_Limit_Group_Name";
            Relation["PromotionLimitGroupInfo.Promotion_Limit_Group_Site"] = "Promotion_Limit_Group.Promotion_Limit_Group_Site";

            //Promotion_Limit
            Relation["PromotionLimitInfo.Promotion_Limit_ID"] = "Promotion_Limit.Promotion_Limit_ID";
            Relation["PromotionLimitInfo.Promotion_Limit_GroupID"] = "Promotion_Limit.Promotion_Limit_GroupID";
            Relation["PromotionLimitInfo.Promotion_Limit_ProductID"] = "Promotion_Limit.Promotion_Limit_ProductID";
            Relation["PromotionLimitInfo.Promotion_Limit_Price"] = "Promotion_Limit.Promotion_Limit_Price";
            Relation["PromotionLimitInfo.Promotion_Limit_Amount"] = "Promotion_Limit.Promotion_Limit_Amount";
            Relation["PromotionLimitInfo.Promotion_Limit_Limit"] = "Promotion_Limit.Promotion_Limit_Limit";
            Relation["PromotionLimitInfo.Promotion_Limit_Starttime"] = "Promotion_Limit.Promotion_Limit_Starttime";
            Relation["PromotionLimitInfo.Promotion_Limit_Endtime"] = "Promotion_Limit.Promotion_Limit_Endtime";
            Relation["PromotionLimitInfo.Promotion_Limit_Site"] = "Promotion_Limit.Promotion_Limit_Site";

            //Promotion_WholeSale_Group
            Relation["PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_ID"] = "Promotion_WholeSale_Group.Promotion_WholeSale_Group_ID";
            Relation["PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Name"] = "Promotion_WholeSale_Group.Promotion_WholeSale_Group_Name";
            Relation["PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Site"] = "Promotion_WholeSale_Group.Promotion_WholeSale_Group_Site";
            Relation["PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_IsActive"] = "Promotion_WholeSale_Group.Promotion_WholeSale_Group_IsActive";
            Relation["PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Percent"] = "Promotion_WholeSale_Group.Promotion_WholeSale_Group_Percent";
            Relation["PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Limit"] = "Promotion_WholeSale_Group.Promotion_WholeSale_Group_Limit";


            //Promotion_WholeSale
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_ID"] = "Promotion_WholeSale.Promotion_WholeSale_ID";
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_GroupID"] = "Promotion_WholeSale.Promotion_WholeSale_GroupID";
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_ProductID"] = "Promotion_WholeSale.Promotion_WholeSale_ProductID";
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_Price"] = "Promotion_WholeSale.Promotion_WholeSale_Price";
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_MinAmount"] = "Promotion_WholeSale.Promotion_WholeSale_MinAmount";
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_Site"] = "Promotion_WholeSale.Promotion_WholeSale_Site";

            //Promotion_Favor_Coupon
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_ID"] = "Promotion_Favor_Coupon.Promotion_Coupon_ID";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Title"] = "Promotion_Favor_Coupon.Promotion_Coupon_Title";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Target"] = "Promotion_Favor_Coupon.Promotion_Coupon_Target";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Payline"] = "Promotion_Favor_Coupon.Promotion_Coupon_Payline";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Manner"] = "Promotion_Favor_Coupon.Promotion_Coupon_Manner";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Price"] = "Promotion_Favor_Coupon.Promotion_Coupon_Price";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Percent"] = "Promotion_Favor_Coupon.Promotion_Coupon_Percent";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Amount"] = "Promotion_Favor_Coupon.Promotion_Coupon_Amount";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Starttime"] = "Promotion_Favor_Coupon.Promotion_Coupon_Starttime";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Endtime"] = "Promotion_Favor_Coupon.Promotion_Coupon_Endtime";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Member_ID"] = "Promotion_Favor_Coupon.Promotion_Coupon_Member_ID";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Code"] = "Promotion_Favor_Coupon.Promotion_Coupon_Code";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Verifycode"] = "Promotion_Favor_Coupon.Promotion_Coupon_Verifycode";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Isused"] = "Promotion_Favor_Coupon.Promotion_Coupon_Isused";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_UseAmount"] = "Promotion_Favor_Coupon.Promotion_Coupon_UseAmount";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Display"] = "Promotion_Favor_Coupon.Promotion_Coupon_Display";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_OrdersID"] = "Promotion_Favor_Coupon.Promotion_Coupon_OrdersID";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Note"] = "Promotion_Favor_Coupon.Promotion_Coupon_Note";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Addtime"] = "Promotion_Favor_Coupon.Promotion_Coupon_Addtime";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Site"] = "Promotion_Favor_Coupon.Promotion_Coupon_Site";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_PartnerID"] = "Promotion_Favor_Coupon.Promotion_Coupon_PartnerID";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Type"] = "Promotion_Favor_Coupon.Promotion_Coupon_Type";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_img"] = "Promotion_Favor_Coupon.Promotion_Coupon_img";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Notice"] = "Promotion_Favor_Coupon.Promotion_Coupon_Notice";

            //Sources
            Relation["SourcesInfo.Sources_ID"] = "Sources.Sources_ID";
            Relation["SourcesInfo.Sources_Name"] = "Sources.Sources_Name";
            Relation["SourcesInfo.Sources_Code"] = "Sources.Sources_Code";
            Relation["SourcesInfo.Sources_Site"] = "Sources.Sources_Site";

            //Promotion_Coupon_Rule
            Relation["PromotionCouponRuleInfo.Coupon_Rule_ID"] = "Promotion_Coupon_Rule.Coupon_Rule_ID";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Title"] = "Promotion_Coupon_Rule.Coupon_Rule_Title";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Target"] = "Promotion_Coupon_Rule.Coupon_Rule_Target";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Payline"] = "Promotion_Coupon_Rule.Coupon_Rule_Payline";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Manner"] = "Promotion_Coupon_Rule.Coupon_Rule_Manner";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Price"] = "Promotion_Coupon_Rule.Coupon_Rule_Price";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Percent"] = "Promotion_Coupon_Rule.Coupon_Rule_Percent";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Amount"] = "Promotion_Coupon_Rule.Coupon_Rule_Amount";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Valid"] = "Promotion_Coupon_Rule.Coupon_Rule_Valid";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Note"] = "Promotion_Coupon_Rule.Coupon_Rule_Note";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Site"] = "Promotion_Coupon_Rule.Coupon_Rule_Site";

            //Promotion_Favor_Policy
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_ID"] = "Promotion_Favor_Policy.Promotion_Policy_ID";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Title"] = "Promotion_Favor_Policy.Promotion_Policy_Title";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Target"] = "Promotion_Favor_Policy.Promotion_Policy_Target";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Payline"] = "Promotion_Favor_Policy.Promotion_Policy_Payline";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Manner"] = "Promotion_Favor_Policy.Promotion_Policy_Manner";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_CouponRuleID"] = "Promotion_Favor_Policy.Promotion_Policy_CouponRuleID";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Price"] = "Promotion_Favor_Policy.Promotion_Policy_Price";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Percent"] = "Promotion_Favor_Policy.Promotion_Policy_Percent";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Group"] = "Promotion_Favor_Policy.Promotion_Policy_Group";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Limit"] = "Promotion_Favor_Policy.Promotion_Policy_Limit";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_IsRepeat"] = "Promotion_Favor_Policy.Promotion_Policy_IsRepeat";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Starttime"] = "Promotion_Favor_Policy.Promotion_Policy_Starttime";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Endtime"] = "Promotion_Favor_Policy.Promotion_Policy_Endtime";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Sort"] = "Promotion_Favor_Policy.Promotion_Policy_Sort";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_IsActive"] = "Promotion_Favor_Policy.Promotion_Policy_IsActive";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_IsChecked"] = "Promotion_Favor_Policy.Promotion_Policy_IsChecked";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Note"] = "Promotion_Favor_Policy.Promotion_Policy_Note";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Site"] = "Promotion_Favor_Policy.Promotion_Policy_Site";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_IsFirst"] = "Promotion_Favor_Policy.Promotion_Policy_IsFirst";

            //Product_Notify
            Relation["ProductNotifyInfo.Product_Notify_ID"] = "Product_Notify.Product_Notify_ID";
            Relation["ProductNotifyInfo.Product_Notify_MemberID"] = "Product_Notify.Product_Notify_MemberID";
            Relation["ProductNotifyInfo.Product_Notify_Email"] = "Product_Notify.Product_Notify_Email";
            Relation["ProductNotifyInfo.Product_Notify_Mobile"] = "Product_Notify.Product_Notify_Mobile";
            Relation["ProductNotifyInfo.Product_Notify_ProductID"] = "Product_Notify.Product_Notify_ProductID";
            Relation["ProductNotifyInfo.Product_Notify_IsNotify"] = "Product_Notify.Product_Notify_IsNotify";
            Relation["ProductNotifyInfo.Product_Notify_Addtime"] = "Product_Notify.Product_Notify_Addtime";
            Relation["ProductNotifyInfo.Product_Notify_Site"] = "Product_Notify.Product_Notify_Site";


            //Promotion_Favor_Gift
            Relation["PromotionFavorGiftInfo.Promotion_Gift_ID"] = "Promotion_Favor_Gift.Promotion_Gift_ID";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Title"] = "Promotion_Favor_Gift.Promotion_Gift_Title";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Target"] = "Promotion_Favor_Gift.Promotion_Gift_Target";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Group"] = "Promotion_Favor_Gift.Promotion_Gift_Group";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Limit"] = "Promotion_Favor_Gift.Promotion_Gift_Limit";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Starttime"] = "Promotion_Favor_Gift.Promotion_Gift_Starttime";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Endtime"] = "Promotion_Favor_Gift.Promotion_Gift_Endtime";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Addtime"] = "Promotion_Favor_Gift.Promotion_Gift_Addtime";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Sort"] = "Promotion_Favor_Gift.Promotion_Gift_Sort";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_IsRepeat"] = "Promotion_Favor_Gift.Promotion_Gift_IsRepeat";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_IsActive"] = "Promotion_Favor_Gift.Promotion_Gift_IsActive";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_IsChecked"] = "Promotion_Favor_Gift.Promotion_Gift_IsChecked";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Site"] = "Promotion_Favor_Gift.Promotion_Gift_Site";


            //Orders_BackApply
            Relation["OrdersBackApplyInfo.Orders_BackApply_ID"] = "Orders_BackApply.Orders_BackApply_ID";
            Relation["OrdersBackApplyInfo.Orders_BackApply_OrdersCode"] = "Orders_BackApply.Orders_BackApply_OrdersCode";
            Relation["OrdersBackApplyInfo.Orders_BackApply_MemberID"] = "Orders_BackApply.Orders_BackApply_MemberID";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Name"] = "Orders_BackApply.Orders_BackApply_Name";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Type"] = "Orders_BackApply.Orders_BackApply_Type";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Amount"] = "Orders_BackApply.Orders_BackApply_Amount";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Note"] = "Orders_BackApply.Orders_BackApply_Note";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Status"] = "Orders_BackApply.Orders_BackApply_Status";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Addtime"] = "Orders_BackApply.Orders_BackApply_Addtime";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Site"] = "Orders_BackApply.Orders_BackApply_Site";
            Relation["OrdersBackApplyInfo.U_Orders_BackApply_Address"] = "Orders_BackApply.U_Orders_BackApply_Address";
            Relation["OrdersBackApplyInfo.U_Orders_BackApply_CompanyName"] = "Orders_BackApply.U_Orders_BackApply_CompanyName";
            Relation["OrdersBackApplyInfo.U_Orders_BackApply_DeliveryCode"] = "Orders_BackApply.U_Orders_BackApply_DeliveryCode";
            Relation["OrdersBackApplyInfo.U_Orders_BackApply_BankAcount"] = "Orders_BackApply.U_Orders_BackApply_BankAcount";


            //Sys_Menu
            Relation["SysMenuInfo.Sys_Menu_ID"] = "Sys_Menu.Sys_Menu_ID";
            Relation["SysMenuInfo.Sys_Menu_Channel"] = "Sys_Menu.Sys_Menu_Channel";
            Relation["SysMenuInfo.Sys_Menu_Name"] = "Sys_Menu.Sys_Menu_Name";
            Relation["SysMenuInfo.Sys_Menu_ParentID"] = "Sys_Menu.Sys_Menu_ParentID";
            Relation["SysMenuInfo.Sys_Menu_Privilege"] = "Sys_Menu.Sys_Menu_Privilege";
            Relation["SysMenuInfo.Sys_Menu_Icon"] = "Sys_Menu.Sys_Menu_Icon";
            Relation["SysMenuInfo.Sys_Menu_Url"] = "Sys_Menu.Sys_Menu_Url";
            Relation["SysMenuInfo.Sys_Menu_Target"] = "Sys_Menu.Sys_Menu_Target";
            Relation["SysMenuInfo.Sys_Menu_IsSystem"] = "Sys_Menu.Sys_Menu_IsSystem";
            Relation["SysMenuInfo.Sys_Menu_IsDefault"] = "Sys_Menu.Sys_Menu_IsDefault";
            Relation["SysMenuInfo.Sys_Menu_IsCommon"] = "Sys_Menu.Sys_Menu_IsCommon";
            Relation["SysMenuInfo.Sys_Menu_IsActive"] = "Sys_Menu.Sys_Menu_IsActive";
            Relation["SysMenuInfo.Sys_Menu_Sort"] = "Sys_Menu.Sys_Menu_Sort";
            Relation["SysMenuInfo.Sys_Menu_Site"] = "Sys_Menu.Sys_Menu_Site";

            //Supplier
            Relation["SupplierInfo.Supplier_ID"] = "Supplier.Supplier_ID";
            Relation["SupplierInfo.Supplier_Account"] = "Supplier.Supplier_Account";
            Relation["SupplierInfo.Supplier_Password"] = "Supplier.Supplier_Password";
            Relation["SupplierInfo.Supplier_Name"] = "Supplier.Supplier_Name";
            Relation["SupplierInfo.Supplier_Phone"] = "Supplier.Supplier_Phone";
            Relation["SupplierInfo.Supplier_Fax"] = "Supplier.Supplier_Fax";
            Relation["SupplierInfo.Supplier_Contactman"] = "Supplier.Supplier_Contactman";
            Relation["SupplierInfo.Supplier_Address"] = "Supplier.Supplier_Address";
            Relation["SupplierInfo.Supplier_Mode"] = "Supplier.Supplier_Mode";
            Relation["SupplierInfo.Supplier_DeliveryMode"] = "Supplier.Supplier_DeliveryMode";
            Relation["SupplierInfo.Supplier_Status"] = "Supplier.Supplier_Status";
            Relation["SupplierInfo.Supplier_Addtime"] = "Supplier.Supplier_Addtime";
            Relation["SupplierInfo.Supplier_Site"] = "Supplier.Supplier_Site";
            Relation["SupplierInfo.Supplier_Number"] = "Supplier.Supplier_Number";

            //Supplier_Commission_Category
            Relation["SupplierCommissionCategoryInfo.Supplier_Commission_Cate_ID"] = "Supplier_Commission_Category.Supplier_Commission_Cate_ID";
            Relation["SupplierCommissionCategoryInfo.Supplier_Commission_Cate_SupplierID"] = "Supplier_Commission_Category.Supplier_Commission_Cate_SupplierID";
            Relation["SupplierCommissionCategoryInfo.Supplier_Commission_Cate_Name"] = "Supplier_Commission_Category.Supplier_Commission_Cate_Name";
            Relation["SupplierCommissionCategoryInfo.Supplier_Commission_Cate_Amount"] = "Supplier_Commission_Category.Supplier_Commission_Cate_Amount";
            Relation["SupplierCommissionCategoryInfo.Supplier_Commission_Cate_Site"] = "Supplier_Commission_Category.Supplier_Commission_Cate_Site";

            //Supplier_Message
            Relation["SupplierMessageInfo.Supplier_Message_ID"] = "Supplier_Message.Supplier_Message_ID";
            Relation["SupplierMessageInfo.Supplier_Message_SupplierID"] = "Supplier_Message.Supplier_Message_SupplierID";
            Relation["SupplierMessageInfo.Supplier_Message_Title"] = "Supplier_Message.Supplier_Message_Title";
            Relation["SupplierMessageInfo.Supplier_Message_Content"] = "Supplier_Message.Supplier_Message_Content";
            Relation["SupplierMessageInfo.Supplier_Message_Addtime"] = "Supplier_Message.Supplier_Message_Addtime";
            Relation["SupplierMessageInfo.Supplier_Message_Site"] = "Supplier_Message.Supplier_Message_Site";

            //Member_Account_Log
            Relation["MemberAccountLogInfo.Account_Log_ID"] = "Member_Account_Log.Account_Log_ID";
            Relation["MemberAccountLogInfo.Account_Log_MemberID"] = "Member_Account_Log.Account_Log_MemberID";
            Relation["MemberAccountLogInfo.Account_Log_Amount"] = "Member_Account_Log.Account_Log_Amount";
            Relation["MemberAccountLogInfo.Account_Log_Remain"] = "Member_Account_Log.Account_Log_Remain";
            Relation["MemberAccountLogInfo.Account_Log_Note"] = "Member_Account_Log.Account_Log_Note";
            Relation["MemberAccountLogInfo.Account_Log_Addtime"] = "Member_Account_Log.Account_Log_Addtime";
            Relation["MemberAccountLogInfo.Account_Log_Site"] = "Member_Account_Log.Account_Log_Site";

            //Article
            Relation["ArticleInfo.Article_ID"] = "Article.Article_ID";
            Relation["ArticleInfo.Article_CateID"] = "Article.Article_CateID";
            Relation["ArticleInfo.Article_Title"] = "Article.Article_Title";
            Relation["ArticleInfo.Article_Source"] = "Article.Article_Source";
            Relation["ArticleInfo.Article_Author"] = "Article.Article_Author";
            Relation["ArticleInfo.Article_Img"] = "Article.Article_Img";
            Relation["ArticleInfo.Article_Keyword"] = "Article.Article_Keyword";
            Relation["ArticleInfo.Article_Intro"] = "Article.Article_Intro";
            Relation["ArticleInfo.Article_Content"] = "Article.Article_Content";
            Relation["ArticleInfo.Article_Addtime"] = "Article.Article_Addtime";
            Relation["ArticleInfo.Article_Hits"] = "Article.Article_Hits";
            Relation["ArticleInfo.Article_IsRecommend"] = "Article.Article_IsRecommend";
            Relation["ArticleInfo.Article_IsAudit"] = "Article.Article_IsAudit";
            Relation["ArticleInfo.Article_Sort"] = "Article.Article_Sort";
            Relation["ArticleInfo.Article_Site"] = "Article.Article_Site";
            Relation["ArticleInfo.Article_Hyperlink"] = "Article.Article_Hyperlink";
            Relation["ArticleInfo.Article_ContentID"] = "Article.Article_ContentID";
            Relation["ArticleInfo.Article_SEO_Title"] = "Article.Article_SEO_Title";
            Relation["ArticleInfo.Article_SEO_Keyword"] = "Article.Article_SEO_Keyword";
            Relation["ArticleInfo.Article_SEO_Description"] = "Article.Article_SEO_Description";
            Relation["ArticleInfo.Article_PageViews"] = "Article.Article_PageViews";
            Relation["ArticleInfo.Artide_ShoulderTitle"] = "Article.Artide_ShoulderTitle";
            Relation["ArticleInfo.Artide_ShoulderTitleSize"] = "Article.Artide_ShoulderTitleSize";
            Relation["ArticleInfo.Article_HyperlinkSize"] = "Article.Article_HyperlinkSize";
            Relation["ArticleInfo.Artide_IsTop"] = "Article.Artide_IsTop";
            Relation["ArticleInfo.Subject_ID"] = "Article.Subject_ID";
            Relation["ArticleInfo.Artide_SouceType"] = "Article.Artide_SouceType";
            Relation["ArticleInfo.Article_memberID"] = "Article.Article_memberID";

            //Article_Cate
            Relation["ArticleCateInfo.Article_Cate_ID"] = "Article_Cate.Article_Cate_ID";
            Relation["ArticleCateInfo.Article_Cate_ParentID"] = "Article_Cate.Article_Cate_ParentID";
            Relation["ArticleCateInfo.Article_Cate_Name"] = "Article_Cate.Article_Cate_Name";
            Relation["ArticleCateInfo.Article_Cate_Sort"] = "Article_Cate.Article_Cate_Sort";
            Relation["ArticleCateInfo.Article_Cate_Site"] = "Article_Cate.Article_Cate_Site";
            Relation["ArticleCateInfo.Article_Cate_Href"] = "Article_Cate.Article_Cate_Href";
            Relation["ArticleCateInfo.Article_Cate_SEO_Title"] = "Article_Cate.Article_Cate_SEO_Title";
            Relation["ArticleCateInfo.Article_Cate_SEO_Keyword"] = "Article_Cate.Article_Cate_SEO_Keyword";
            Relation["ArticleCateInfo.Article_Cate_SEO_Description"] = "Article_Cate.Article_Cate_SEO_Description";
            Relation["ArticleCateInfo.Article_Cate_IsTop"] = "Article_Cate.Article_Cate_IsTop";
            Relation["ArticleCateInfo.Article_Cate_Type"] = "Article_Cate.Article_Cate_Type";

            //Home_Left_Cate
            Relation["HomeLeftCateInfo.Home_Left_Cate_ID"] = "Home_Left_Cate.Home_Left_Cate_ID";
            Relation["HomeLeftCateInfo.Home_Left_Cate_ParentID"] = "Home_Left_Cate.Home_Left_Cate_ParentID";
            Relation["HomeLeftCateInfo.Home_Left_Cate_Name"] = "Home_Left_Cate.Home_Left_Cate_Name";
            Relation["HomeLeftCateInfo.Home_Left_Cate_URL"] = "Home_Left_Cate.Home_Left_Cate_URL";
            Relation["HomeLeftCateInfo.Home_Left_Cate_Img"] = "Home_Left_Cate.Home_Left_Cate_Img";
            Relation["HomeLeftCateInfo.Home_Left_Cate_Sort"] = "Home_Left_Cate.Home_Left_Cate_Sort";
            Relation["HomeLeftCateInfo.Home_Left_Cate_Active"] = "Home_Left_Cate.Home_Left_Cate_Active";
            Relation["HomeLeftCateInfo.Home_Left_Cate_Site"] = "Home_Left_Cate.Home_Left_Cate_Site";

            //RBAC_User_Log
            Relation["RBACUserLogInfo.Log_ID"] = "RBAC_User_Log.Log_ID";
            Relation["RBACUserLogInfo.Log_Channel"] = "RBAC_User_Log.Log_Channel";
            Relation["RBACUserLogInfo.Log_UserID"] = "RBAC_User_Log.Log_UserID";
            Relation["RBACUserLogInfo.Log_UserName"] = "RBAC_User_Log.Log_UserName";
            Relation["RBACUserLogInfo.Log_User_ObjectID"] = "RBAC_User_Log.Log_User_ObjectID";
            Relation["RBACUserLogInfo.Log_Action"] = "RBAC_User_Log.Log_Action";
            Relation["RBACUserLogInfo.Log_Description"] = "RBAC_User_Log.Log_Description";
            Relation["RBACUserLogInfo.Log_Result"] = "RBAC_User_Log.Log_Result";
            Relation["RBACUserLogInfo.Log_IP"] = "RBAC_User_Log.Log_IP";
            Relation["RBACUserLogInfo.Log_Addtime"] = "RBAC_User_Log.Log_Addtime";
            Relation["RBACUserLogInfo.Log_Site"] = "RBAC_User_Log.Log_Site";

            //RBAC_User_Log_Channel
            Relation["RBACUserLogChannelInfo.Log_Channel_ID"] = "RBAC_User_Log_Channel.Log_Channel_ID";
            Relation["RBACUserLogChannelInfo.Log_Channel_Name"] = "RBAC_User_Log_Channel.Log_Channel_Name";
            Relation["RBACUserLogChannelInfo.Log_Channel_ParentID"] = "RBAC_User_Log_Channel.Log_Channel_ParentID";
            Relation["RBACUserLogChannelInfo.Log_Channel_Type"] = "RBAC_User_Log_Channel.Log_Channel_Type";

            //Promotion_Tag
            Relation["PromotionTagInfo.Promotion_Tag_ID"] = "Promotion_Tag.Promotion_Tag_ID";
            Relation["PromotionTagInfo.Promotion_Tag_Name"] = "Promotion_Tag.Promotion_Tag_Name";
            Relation["PromotionTagInfo.Promotion_Tag_Img"] = "Promotion_Tag.Promotion_Tag_Img";
            Relation["PromotionTagInfo.Promotion_Tag_Note"] = "Promotion_Tag.Promotion_Tag_Note";
            Relation["PromotionTagInfo.Promotion_Tag_IsActive"] = "Promotion_Tag.Promotion_Tag_IsActive";
            Relation["PromotionTagInfo.Promotion_Tag_Trash"] = "Promotion_Tag.Promotion_Tag_Trash";
            Relation["PromotionTagInfo.Promotion_Tag_Sort"] = "Promotion_Tag.Promotion_Tag_Sort";
            Relation["PromotionTagInfo.Promotion_Tag_Site"] = "Promotion_Tag.Promotion_Tag_Site";

            #region 礼品卡

            //Member_Card
            Relation["MemberCardInfo.Member_Card_ID"] = "Member_Card.Member_Card_ID";
            Relation["MemberCardInfo.Member_Card_ParentID"] = "Member_Card.Member_Card_ParentID";
            Relation["MemberCardInfo.Member_Card_Code"] = "Member_Card.Member_Card_Code";
            Relation["MemberCardInfo.Member_Card_Password"] = "Member_Card.Member_Card_Password";
            Relation["MemberCardInfo.Member_Card_Name"] = "Member_Card.Member_Card_Name";
            Relation["MemberCardInfo.Member_Card_Batch"] = "Member_Card.Member_Card_Batch";
            Relation["MemberCardInfo.Member_Card_Balance"] = "Member_Card.Member_Card_Balance";
            Relation["MemberCardInfo.Member_Card_UserID"] = "Member_Card.Member_Card_UserID";
            Relation["MemberCardInfo.Member_Card_EffectiveTime"] = "Member_Card.Member_Card_EffectiveTime";
            Relation["MemberCardInfo.Member_Card_Starttime"] = "Member_Card.Member_Card_Starttime";
            Relation["MemberCardInfo.Member_Card_Endtime"] = "Member_Card.Member_Card_Endtime";
            Relation["MemberCardInfo.Member_Card_Addtime"] = "Member_Card.Member_Card_Addtime";
            Relation["MemberCardInfo.Member_Card_TypeID"] = "Member_Card.Member_Card_TypeID";
            Relation["MemberCardInfo.Member_Card_IsActive"] = "Member_Card.Member_Card_IsActive";
            Relation["MemberCardInfo.Member_Card_IsState"] = "Member_Card.Member_Card_IsState";
            Relation["MemberCardInfo.Member_Card_IsShow"] = "Member_Card.Member_Card_IsShow";
            Relation["MemberCardInfo.Member_Card_OrderDate"] = "Member_Card.Member_Card_OrderDate";
            Relation["MemberCardInfo.Member_Card_MaxDeliveryAmount"] = "Member_Card.Member_Card_MaxDeliveryAmount";
            Relation["MemberCardInfo.Member_Card_AddressName"] = "Member_Card.Member_Card_AddressName";
            Relation["MemberCardInfo.Member_Card_AddressCountry"] = "Member_Card.Member_Card_AddressCountry";
            Relation["MemberCardInfo.Member_Card_AddressState"] = "Member_Card.Member_Card_AddressState";
            Relation["MemberCardInfo.Member_Card_AddressCity"] = "Member_Card.Member_Card_AddressCity";
            Relation["MemberCardInfo.Member_Card_AddressCounty"] = "Member_Card.Member_Card_AddressCounty";
            Relation["MemberCardInfo.Member_Card_AddressDetail"] = "Member_Card.Member_Card_AddressDetail";
            Relation["MemberCardInfo.Member_Card_AddressZip"] = "Member_Card.Member_Card_AddressZip";
            Relation["MemberCardInfo.Member_Card_AddressTel"] = "Member_Card.Member_Card_AddressTel";
            Relation["MemberCardInfo.Member_Card_Site"] = "Member_Card.Member_Card_Site";
            Relation["MemberCardInfo.Member_Card_Salesman"] = "Member_Card.Member_Card_Salesman";

            //Member_Card_Type
            Relation["MemberCardTypeInfo.Member_Card_Type_ID"] = "Member_Card_Type.Member_Card_Type_ID";
            Relation["MemberCardTypeInfo.Member_Card_Type_Category"] = "Member_Card_Type.Member_Card_Type_Category";
            Relation["MemberCardTypeInfo.Member_Card_Type_Name"] = "Member_Card_Type.Member_Card_Type_Name";
            Relation["MemberCardTypeInfo.Member_Card_Type_Quota"] = "Member_Card_Type.Member_Card_Type_Quota";
            Relation["MemberCardTypeInfo.Member_Card_Type_Discount"] = "Member_Card_Type.Member_Card_Type_Discount";
            Relation["MemberCardTypeInfo.Member_Card_Type_IsRecharge"] = "Member_Card_Type.Member_Card_Type_IsRecharge";
            Relation["MemberCardTypeInfo.Member_Card_Type_UseNember"] = "Member_Card_Type.Member_Card_Type_UseNember";
            Relation["MemberCardTypeInfo.Member_Card_Type_MaxExpend"] = "Member_Card_Type.Member_Card_Type_MaxExpend";
            Relation["MemberCardTypeInfo.Member_Card_Type_Site"] = "Member_Card_Type.Member_Card_Type_Site";
            Relation["MemberCardTypeInfo.Member_Card_Type_SubCardNum"] = "Member_Card_Type.Member_Card_Type_SubCardNum";

            //Member_Card_Log
            Relation["MemberCardLogInfo.Member_Card_Log_ID"] = "Member_Card_Log.Member_Card_Log_ID";
            Relation["MemberCardLogInfo.Member_Card_Log_CardID"] = "Member_Card_Log.Member_Card_Log_CardID";
            Relation["MemberCardLogInfo.Member_Card_Log_Addtime"] = "Member_Card_Log.Member_Card_Log_Addtime";
            Relation["MemberCardLogInfo.Member_Card_Log_Price"] = "Member_Card_Log.Member_Card_Log_Price";
            Relation["MemberCardLogInfo.Member_Card_Log_Action"] = "Member_Card_Log.Member_Card_Log_Action";
            Relation["MemberCardLogInfo.Member_Card_Log_Remark"] = "Member_Card_Log.Member_Card_Log_Remark";
            Relation["MemberCardLogInfo.Member_Card_Log_Operator"] = "Member_Card_Log.Member_Card_Log_Operator";

            //Member_Card_Orders
            Relation["MemberCardOrdersInfo.Card_Orders_ID"] = "Member_Card_Orders.Card_Orders_ID";
            Relation["MemberCardOrdersInfo.Card_Orders_SN"] = "Member_Card_Orders.Card_Orders_SN";
            Relation["MemberCardOrdersInfo.Card_Orders_Amount"] = "Member_Card_Orders.Card_Orders_Amount";
            Relation["MemberCardOrdersInfo.Card_Orders_CardID"] = "Member_Card_Orders.Card_Orders_CardID";
            Relation["MemberCardOrdersInfo.Card_Orders_Payway"] = "Member_Card_Orders.Card_Orders_Payway";
            Relation["MemberCardOrdersInfo.Card_Orders_Status"] = "Member_Card_Orders.Card_Orders_Status";
            Relation["MemberCardOrdersInfo.Card_Orders_Note"] = "Member_Card_Orders.Card_Orders_Note";
            Relation["MemberCardOrdersInfo.Card_Orders_Addtime"] = "Member_Card_Orders.Card_Orders_Addtime";
            Relation["MemberCardOrdersInfo.Card_Orders_Type"] = "Member_Card_Orders.Card_Orders_Type";
            Relation["MemberCardOrdersInfo.Card_Orders_Reason"] = "Member_Card_Orders.Card_Orders_Reason";

            //Member_Card_Orders_Log
            Relation["MemberCardOrdersLogInfo.Card_Orders_Log_ID"] = "Member_Card_Orders_Log.Card_Orders_Log_ID";
            Relation["MemberCardOrdersLogInfo.Card_Orders_Log_OrdersID"] = "Member_Card_Orders_Log.Card_Orders_Log_OrdersID";
            Relation["MemberCardOrdersLogInfo.Card_Orders_Log_Addtime"] = "Member_Card_Orders_Log.Card_Orders_Log_Addtime";
            Relation["MemberCardOrdersLogInfo.Card_Orders_Log_Action"] = "Member_Card_Orders_Log.Card_Orders_Log_Action";
            Relation["MemberCardOrdersLogInfo.Card_Orders_Log_Remark"] = "Member_Card_Orders_Log.Card_Orders_Log_Remark";
            Relation["MemberCardOrdersLogInfo.Card_Orders_Log_Operator"] = "Member_Card_Orders_Log.Card_Orders_Log_Operator";

            //Member_Card_Type_Product
            Relation["MemberCardTypeProductInfo.Relate_ID"] = "Member_Card_Type_Product.Relate_ID";
            Relation["MemberCardTypeProductInfo.CardType_ID"] = "Member_Card_Type_Product.CardType_ID";
            Relation["MemberCardTypeProductInfo.Product_ID"] = "Member_Card_Type_Product.Product_ID";



            #endregion


            //MobileNumberGrade
            Relation["MobileNumberGradeInfo.Id"] = "MobileNumberGrade.Id";
            Relation["MobileNumberGradeInfo.Name"] = "MobileNumberGrade.Name";
            Relation["MobileNumberGradeInfo.Addtime"] = "MobileNumberGrade.Addtime";
            Relation["MobileNumberGradeInfo.Site"] = "MobileNumberGrade.Site";

            //MobileNumber
            Relation["MobileNumberInfo.Id"] = "MobileNumber.Id";
            Relation["MobileNumberInfo.Operators"] = "MobileNumber.Operators";
            Relation["MobileNumberInfo.Network"] = "MobileNumber.Network";
            Relation["MobileNumberInfo.StateCode"] = "MobileNumber.StateCode";
            Relation["MobileNumberInfo.CityCode"] = "MobileNumber.CityCode";
            Relation["MobileNumberInfo.GradeId"] = "MobileNumber.GradeId";
            Relation["MobileNumberInfo.MinPrice"] = "MobileNumber.MinPrice";
            Relation["MobileNumberInfo.YcPrice"] = "MobileNumber.YcPrice";
            Relation["MobileNumberInfo.Number"] = "MobileNumber.Number";
            Relation["MobileNumberInfo.Addtime"] = "MobileNumber.Addtime";
            Relation["MobileNumberInfo.Site"] = "MobileNumber.Site";
            Relation["MobileNumberInfo.Status"] = "MobileNumber.Status";

            //Store
            Relation["StoreInfo.ID"] = "Store.ID";
            Relation["StoreInfo.PartnersID"] = "Store.PartnersID";
            Relation["StoreInfo.SN"] = "Store.SN";
            Relation["StoreInfo.Name"] = "Store.Name";
            Relation["StoreInfo.State"] = "Store.State";
            Relation["StoreInfo.City"] = "Store.City";
            Relation["StoreInfo.County"] = "Store.County";
            Relation["StoreInfo.Address"] = "Store.Address";
            Relation["StoreInfo.Linkman"] = "Store.Linkman";
            Relation["StoreInfo.Phone"] = "Store.Phone";
            Relation["StoreInfo.Mobile"] = "Store.Mobile";
            Relation["StoreInfo.Longitude"] = "Store.Longitude";
            Relation["StoreInfo.Latitude"] = "Store.Latitude";
            Relation["StoreInfo.Img"] = "Store.Img";
            Relation["StoreInfo.Business"] = "Store.Business";
            Relation["StoreInfo.OpenTime"] = "Store.OpenTime";
            Relation["StoreInfo.Addtime"] = "Store.Addtime";
            Relation["StoreInfo.Intro"] = "Store.Intro";

            //Member_Validate
            Relation["MemberValidateInfo.ID"] = "Member_Validate.ID";
            Relation["MemberValidateInfo.MemberID"] = "Member_Validate.MemberID";
            Relation["MemberValidateInfo.DataName"] = "Member_Validate.DataName";
            Relation["MemberValidateInfo.TempData"] = "Member_Validate.TempData";
            Relation["MemberValidateInfo.VerifyCode"] = "Member_Validate.VerifyCode";
            Relation["MemberValidateInfo.Addtime"] = "Member_Validate.Addtime";

            //Product_TaoCan
            Relation["ProductTaoCanInfo.Id"] = "Product_TaoCan.Id";
            Relation["ProductTaoCanInfo.YunYingShang"] = "Product_TaoCan.YunYingShang";
            Relation["ProductTaoCanInfo.PinPai"] = "Product_TaoCan.PinPai";
            Relation["ProductTaoCanInfo.StateCode"] = "Product_TaoCan.StateCode";
            Relation["ProductTaoCanInfo.CityCode"] = "Product_TaoCan.CityCode";
            Relation["ProductTaoCanInfo.YueFei"] = "Product_TaoCan.YueFei";
            Relation["ProductTaoCanInfo.WangBie"] = "Product_TaoCan.WangBie";
            Relation["ProductTaoCanInfo.Name"] = "Product_TaoCan.Name";
            Relation["ProductTaoCanInfo.Note"] = "Product_TaoCan.Note";
            Relation["ProductTaoCanInfo.AddTime"] = "Product_TaoCan.AddTime";
            Relation["ProductTaoCanInfo.Site"] = "Product_TaoCan.Site";

            //Product_HeYue_TaoCan
            Relation["ProductHeYueTaoCanInfo.Id"] = "Product_HeYue_TaoCan.Id";
            Relation["ProductHeYueTaoCanInfo.HeYueId"] = "Product_HeYue_TaoCan.HeYueId";
            Relation["ProductHeYueTaoCanInfo.TaoCanId"] = "Product_HeYue_TaoCan.TaoCanId";
            Relation["ProductHeYueTaoCanInfo.TaoCanName"] = "Product_HeYue_TaoCan.TaoCanName";
            Relation["ProductHeYueTaoCanInfo.Site"] = "Product_HeYue_TaoCan.Site";

            //Product_HeYue_NumberGrade
            Relation["ProductHeYueNumberGradeInfo.Id"] = "Product_HeYue_NumberGrade.Id";
            Relation["ProductHeYueNumberGradeInfo.HeYueId"] = "Product_HeYue_NumberGrade.HeYueId";
            Relation["ProductHeYueNumberGradeInfo.NumberGradeId"] = "Product_HeYue_NumberGrade.NumberGradeId";
            Relation["ProductHeYueNumberGradeInfo.NumberGradeName"] = "Product_HeYue_NumberGrade.NumberGradeName";
            Relation["ProductHeYueNumberGradeInfo.Site"] = "Product_HeYue_NumberGrade.Site";

            //Product_HeYue_Addr
            Relation["ProductHeYueAddrInfo.Id"] = "Product_HeYue_Addr.Id";
            Relation["ProductHeYueAddrInfo.HeYueId"] = "Product_HeYue_Addr.HeYueId";
            Relation["ProductHeYueAddrInfo.StateCode"] = "Product_HeYue_Addr.StateCode";
            Relation["ProductHeYueAddrInfo.CityCode"] = "Product_HeYue_Addr.CityCode";
            Relation["ProductHeYueAddrInfo.StateName"] = "Product_HeYue_Addr.StateName";
            Relation["ProductHeYueAddrInfo.CityName"] = "Product_HeYue_Addr.CityName";
            Relation["ProductHeYueAddrInfo.Site"] = "Product_HeYue_Addr.Site";

            //Product_HeYue
            Relation["ProductHeYueInfo.Id"] = "Product_HeYue.Id";
            Relation["ProductHeYueInfo.ProductId"] = "Product_HeYue.ProductId";
            Relation["ProductHeYueInfo.Name"] = "Product_HeYue.Name";
            Relation["ProductHeYueInfo.YuCunKuan"] = "Product_HeYue.YuCunKuan";
            Relation["ProductHeYueInfo.FanHuanNote"] = "Product_HeYue.FanHuanNote";
            Relation["ProductHeYueInfo.SongFeiNote"] = "Product_HeYue.SongFeiNote";
            Relation["ProductHeYueInfo.HeYueQi"] = "Product_HeYue.HeYueQi";
            Relation["ProductHeYueInfo.HeYueCode"] = "Product_HeYue.HeYueCode";
            Relation["ProductHeYueInfo.Site"] = "Product_HeYue.Site";

            //Member_Jiang_Item
            Relation["MemberJiangItemInfo.Id"] = "Member_Jiang_Item.Id";
            Relation["MemberJiangItemInfo.Name"] = "Member_Jiang_Item.Name";
            Relation["MemberJiangItemInfo.Tip"] = "Member_Jiang_Item.Tip";
            Relation["MemberJiangItemInfo.Total"] = "Member_Jiang_Item.Total";
            Relation["MemberJiangItemInfo.UseNum"] = "Member_Jiang_Item.UseNum";
            Relation["MemberJiangItemInfo.Img"] = "Member_Jiang_Item.Img";
            Relation["MemberJiangItemInfo.CouponRuleId"] = "Member_Jiang_Item.CouponRuleId";
            Relation["MemberJiangItemInfo.Site"] = "Member_Jiang_Item.Site";
            Relation["MemberJiangItemInfo.Url"] = "Member_Jiang_Item.Url";

            //Member_Jiang
            Relation["MemberJiangInfo.Id"] = "Member_Jiang.Id";
            Relation["MemberJiangInfo.MemberId"] = "Member_Jiang.MemberId";
            Relation["MemberJiangInfo.JiangItemId"] = "Member_Jiang.JiangItemId";
            Relation["MemberJiangInfo.JiangItemName"] = "Member_Jiang.JiangItemName";
            Relation["MemberJiangInfo.CouponRuleId"] = "Member_Jiang.CouponRuleId";
            Relation["MemberJiangInfo.AddTime"] = "Member_Jiang.AddTime";
            Relation["MemberJiangInfo.Site"] = "Member_Jiang.Site";


            //U_User
            Relation["UserInfo.User_ID"] = "U_User.User_ID";
            Relation["UserInfo.User_Type"] = "U_User.User_Type";
            Relation["UserInfo.User_Province"] = "U_User.User_Province";
            Relation["UserInfo.User_Name"] = "U_User.User_Name";
            Relation["UserInfo.User_Password"] = "U_User.User_Password";
            Relation["UserInfo.User_AddPower"] = "U_User.User_AddPower";
            Relation["UserInfo.User_EditPower"] = "U_User.User_EditPower";
            Relation["UserInfo.User_DelPower"] = "U_User.User_DelPower";
            Relation["UserInfo.User_AuditPower"] = "U_User.User_AuditPower";
            Relation["UserInfo.User_AddTime"] = "U_User.User_AddTime";
            //U_Activity
            Relation["ActivityInfo.Activity_ID"] = "U_Activity.Activity_ID";
            Relation["ActivityInfo.Activity_Type"] = "U_Activity.Activity_Type";
            Relation["ActivityInfo.Activity_Province"] = "U_Activity.Activity_Province";
            Relation["ActivityInfo.Activity_StartTime"] = "U_Activity.Activity_StartTime";
            Relation["ActivityInfo.Activity_EndTime"] = "U_Activity.Activity_EndTime";
            Relation["ActivityInfo.Activity_ProductType"] = "U_Activity.Activity_ProductType";
            Relation["ActivityInfo.Activity_Title"] = "U_Activity.Activity_Title";
            Relation["ActivityInfo.Activity_Range"] = "U_Activity.Activity_Range";
            Relation["ActivityInfo.Activity_Intro"] = "U_Activity.Activity_Intro";
            Relation["ActivityInfo.Activity_Description"] = "U_Activity.Activity_Description";
            Relation["ActivityInfo.Activity_AddTime"] = "U_Activity.Activity_AddTime";
            Relation["ActivityInfo.Activity_UserID"] = "U_Activity.Activity_UserID";
            Relation["ActivityInfo.Activity_Audit"] = "U_Activity.Activity_Audit";
            Relation["ActivityInfo.Activity_AuditUser"] = "U_Activity.Activity_AuditUser";
            Relation["ActivityInfo.Activity_AuditTime"] = "U_Activity.Activity_AuditTime";
            Relation["ActivityInfo.Activity_Recommend"] = "U_Activity.Activity_Recommend";
            Relation["ActivityInfo.Activity_Img"] = "U_Activity.Activity_Img";
            Relation["ActivityInfo.Activity_Sort"] = "U_Activity.Activity_Sort";
            Relation["ActivityInfo.Activity_IsActive"] = "U_Activity.Activity_IsActive";



            //U_ActivityClick
            Relation["ActivityClickInfo.ActivityClick_ID"] = "U_ActivityClick.ActivityClick_ID";
            Relation["ActivityClickInfo.ActivityClick_ActivityID"] = "U_ActivityClick.ActivityClick_ActivityID";
            Relation["ActivityClickInfo.ActivityClick_Province"] = "U_ActivityClick.ActivityClick_Province";
            Relation["ActivityClickInfo.ActivityClick_ClickTime"] = "U_ActivityClick.ActivityClick_ClickTime";


            //Device_Brand
            Relation["DeviceBrandInfo.Brand_ID"] = "Device_Brand.Brand_ID";
            Relation["DeviceBrandInfo.Brand_Name"] = "Device_Brand.Brand_Name";
            Relation["DeviceBrandInfo.Brand_Sort"] = "Device_Brand.Brand_Sort";
            Relation["DeviceBrandInfo.Brand_Status"] = "Device_Brand.Brand_Status";


            //Device
            Relation["DeviceInfo.Device_ID"] = "Device.Device_ID";
            Relation["DeviceInfo.Device_BrandID"] = "Device.Device_BrandID";
            Relation["DeviceInfo.Device_Name"] = "Device.Device_Name";
            Relation["DeviceInfo.Device_Img"] = "Device.Device_Img";
            Relation["DeviceInfo.Device_Status"] = "Device.Device_Status";
            Relation["DeviceInfo.Device_Addtime"] = "Device.Device_Addtime";
            Relation["DeviceInfo.Device_GroupID"] = "Device.Device_GroupID";

            //Device_Extend
            Relation["DeviceExtendInfo.Device_Extend_ID"] = "Device_Extend.Device_Extend_ID";
            Relation["DeviceExtendInfo.Device_Extend_Name"] = "Device_Extend.Device_Extend_Name";
            Relation["DeviceExtendInfo.Device_Extend_Status"] = "Device_Extend.Device_Extend_Status";
            Relation["DeviceExtendInfo.Device_Extend_Sort"] = "Device_Extend.Device_Extend_Sort";
            Relation["DeviceExtendInfo.Device_Extend_InputType"] = "Device_Extend.Device_Extend_InputType";
            Relation["DeviceExtendInfo.Device_Extend_DefaultVal"] = "Device_Extend.Device_Extend_DefaultVal";

            //Device_RelateExtend
            Relation["DeviceRelateExtendInfo.RelateExtend_ID"] = "Device_RelateExtend.RelateExtend_ID";
            Relation["DeviceRelateExtendInfo.RelateExtend_DeviceID"] = "Device_RelateExtend.RelateExtend_DeviceID";
            Relation["DeviceRelateExtendInfo.RelateExtend_ExtendID"] = "Device_RelateExtend.RelateExtend_ExtendID";
            Relation["DeviceRelateExtendInfo.RelateExtend_Val"] = "Device_RelateExtend.RelateExtend_Val";

            //Product_ApplyExtend
            Relation["ProductApplyExtendInfo.Relate_ID"] = "Product_ApplyExtend.Relate_ID";
            Relation["ProductApplyExtendInfo.Relate_ProductID"] = "Product_ApplyExtend.Relate_ProductID";
            Relation["ProductApplyExtendInfo.Relate_ExtendID"] = "Product_ApplyExtend.Relate_ExtendID";
            Relation["ProductApplyExtendInfo.Relate_Val"] = "Product_ApplyExtend.Relate_Val";

            //Member_Device
            Relation["MemberDeviceInfo.Member_Device_ID"] = "Member_Device.Member_Device_ID";
            Relation["MemberDeviceInfo.Member_Device_MemberID"] = "Member_Device.Member_Device_MemberID";
            Relation["MemberDeviceInfo.Member_Device_DeviceID"] = "Member_Device.Member_Device_DeviceID";
            Relation["MemberDeviceInfo.Member_Device_Addtime"] = "Member_Device.Member_Device_Addtime";
            Relation["MemberDeviceInfo.Member_Device_BrandID"] = "Member_Device.Member_Device_BrandID";
            Relation["MemberDeviceInfo.Member_Device_DeviceName"] = "Member_Device.Member_Device_DeviceName";
            Relation["MemberDeviceInfo.Member_Device_SerialNumber"] = "Member_Device.Member_Device_SerialNumber";
            Relation["MemberDeviceInfo.Member_Device_Buytime"] = "Member_Device.Member_Device_Buytime";
            Relation["MemberDeviceInfo.Member_Device_Remarks"] = "Member_Device.Member_Device_Remarks";
            Relation["MemberDeviceInfo.Member_Device_DealerName"] = "Member_Device.Member_Device_DealerName";
            Relation["MemberDeviceInfo.Member_Device_BrandName"] = "Member_Device.Member_Device_BrandName";

            //Extend_Group
            Relation["ExtendGroupInfo.Extend_Group_ID"] = "Extend_Group.Extend_Group_ID";
            Relation["ExtendGroupInfo.Extend_Group_Name"] = "Extend_Group.Extend_Group_Name";
            Relation["ExtendGroupInfo.Extend_Group_Type"] = "Extend_Group.Extend_Group_Type";
            Relation["ExtendGroupInfo.Extend_Group_IsActive"] = "Extend_Group.Extend_Group_IsActive";


            //Extend_Group_RelateExtend
            Relation["ExtendGroupRelateExtendInfo.Extend_Group_RelateExtend_ID"] = "Extend_Group_RelateExtend.Extend_Group_RelateExtend_ID";
            Relation["ExtendGroupRelateExtendInfo.Extend_GroupID"] = "Extend_Group_RelateExtend.Extend_GroupID";
            Relation["ExtendGroupRelateExtendInfo.Device_ExtendID"] = "Extend_Group_RelateExtend.Device_ExtendID";

           


            //Print_Services
            Relation["PrintServicesInfo.Print_Services_ID"] = "Print_Services.Print_Services_ID";
            Relation["PrintServicesInfo.Print_Services_Name"] = "Print_Services.Print_Services_Name";
            Relation["PrintServicesInfo.Print_Services_Price"] = "Print_Services.Print_Services_Price";
            Relation["PrintServicesInfo.Print_Services_UrgentPrice"] = "Print_Services.Print_Services_UrgentPrice";
            Relation["PrintServicesInfo.Print_Services_UrgentDay"] = "Print_Services.Print_Services_UrgentDay";
            Relation["PrintServicesInfo.Print_Services_ProcessingFee"] = "Print_Services.Print_Services_ProcessingFee";
            Relation["PrintServicesInfo.Print_Services_ProcessingFee_Limit"] = "Print_Services.Print_Services_ProcessingFee_Limit";
            Relation["PrintServicesInfo.Print_Services_Img"] = "Print_Services.Print_Services_Img";
            Relation["PrintServicesInfo.Print_Services_CateID"] = "Print_Services.Print_Services_CateID";
            Relation["PrintServicesInfo.Print_Services_Sort"] = "Print_Services.Print_Services_Sort";



            //Print_Services_Extend
            Relation["PrintServicesExtendInfo.Extend_ID"] = "Print_Services_Extend.Extend_ID";
            Relation["PrintServicesExtendInfo.Extend_Name"] = "Print_Services_Extend.Extend_Name";
            Relation["PrintServicesExtendInfo.Extend_DefaultVal"] = "Print_Services_Extend.Extend_DefaultVal";
            Relation["PrintServicesExtendInfo.Extend_Status"] = "Print_Services_Extend.Extend_Status";



            //Print_Services_RelateExtend
            Relation["PrintServicesRelateExtendInfo.Print_Services_RelateExtend_ID"] = "Print_Services_RelateExtend.Print_Services_RelateExtend_ID";
            Relation["PrintServicesRelateExtendInfo.Print_ServicesID"] = "Print_Services_RelateExtend.Print_ServicesID";
            Relation["PrintServicesRelateExtendInfo.Print_Services_ExtendID"] = "Print_Services_RelateExtend.Print_Services_ExtendID";
            Relation["PrintServicesRelateExtendInfo.Print_Services_Extend_Val"] = "Print_Services_RelateExtend.Print_Services_Extend_Val";


            //Print_Services_Cate
            Relation["PrintServicesCateInfo.Print_Services_Cate_ID"] = "Print_Services_Cate.Print_Services_Cate_ID";
            Relation["PrintServicesCateInfo.Print_Services_Cate_Name"] = "Print_Services_Cate.Print_Services_Cate_Name";
            Relation["PrintServicesCateInfo.Print_Services_Cate_Sort"] = "Print_Services_Cate.Print_Services_Cate_Sort";


            //Software_Basic
            Relation["SoftwareInfo.Software_ID"] = "Software_Basic.Software_ID";
            Relation["SoftwareInfo.Software_CateID"] = "Software_Basic.Software_CateID";
            Relation["SoftwareInfo.Software_Name"] = "Software_Basic.Software_Name";
            Relation["SoftwareInfo.Software_Img"] = "Software_Basic.Software_Img";
            Relation["SoftwareInfo.Software_TimeLength"] = "Software_Basic.Software_TimeLength";
            Relation["SoftwareInfo.Software_DownloadAddress"] = "Software_Basic.Software_DownloadAddress";
            Relation["SoftwareInfo.Software_IsSales"] = "Software_Basic.Software_IsSales";
            Relation["SoftwareInfo.Software_IsActive"] = "Software_Basic.Software_IsActive";
            Relation["SoftwareInfo.Software_Info"] = "Software_Basic.Software_Info";
            Relation["SoftwareInfo.Software_UploadTime"] = "Software_Basic.Software_UploadTime";
            Relation["SoftwareInfo.Software_Sort"] = "Software_Basic.Software_Sort";
            Relation["SoftwareInfo.Software_Downloads"] = "Software_Basic.Software_Downloads";
            Relation["SoftwareInfo.Software_IsRecommended"] = "Software_Basic.Software_IsRecommended";
            Relation["SoftwareInfo.Software_Note"] = "Software_Basic.Software_Note";
            Relation["SoftwareInfo.Software_SystemInfo"] = "Software_Basic.Software_SystemInfo";
            Relation["SoftwareInfo.Software_UID"] = "Software_Basic.Software_UID";
            Relation["SoftwareInfo.Software_Version"] = "Software_Basic.Software_Version";
            Relation["SoftwareInfo.Software_SEO_Title"] = "Software_Basic.Software_SEO_Title";
            Relation["SoftwareInfo.Software_SEO_Keyword"] = "Software_Basic.Software_SEO_Keyword";
            Relation["SoftwareInfo.Software_SEO_Description"] = "Software_Basic.Software_SEO_Description";
            Relation["SoftwareInfo.Software_IsButton"] = "Software_Basic.Software_IsButton";
            Relation["SoftwareInfo.Software_ButtonName"] = "Software_Basic.Software_ButtonName";
            Relation["SoftwareInfo.Software_ButtonAddress"] = "Software_Basic.Software_ButtonAddress";

            //Software_PriceList
            Relation["SoftwarePriceListInfo.PriceList_ID"] = "Software_PriceList.PriceList_ID";
            Relation["SoftwarePriceListInfo.PriceList_SoftwareID"] = "Software_PriceList.PriceList_SoftwareID";
            Relation["SoftwarePriceListInfo.PriceList_TimeLength"] = "Software_PriceList.PriceList_TimeLength";
            Relation["SoftwarePriceListInfo.PriceList_Money"] = "Software_PriceList.PriceList_Money";

            //Member_Group
            Relation["MemberGroupInfo.Group_ID"] = "Member_Group.Group_ID";
            Relation["MemberGroupInfo.Group_MemberID"] = "Member_Group.Group_MemberID";
            Relation["MemberGroupInfo.Group_Name"] = "Member_Group.Group_Name";
            Relation["MemberGroupInfo.Group_IsVisible"] = "Member_Group.Group_IsVisible";
            Relation["MemberGroupInfo.Group_IsPublic"] = "Member_Group.Group_IsPublic";
            Relation["MemberGroupInfo.Group_Grade"] = "Member_Group.Group_Grade";
            Relation["MemberGroupInfo.Group_UpLimit"] = "Member_Group.Group_UpLimit";
            Relation["MemberGroupInfo.Group_AddTime"] = "Member_Group.Group_AddTime";
            Relation["MemberGroupInfo.Group_Note"] = "Member_Group.Group_Note";
            Relation["MemberGroupInfo.Group_IsActive"] = "Member_Group.Group_IsActive";
            //Member_Group_Member
            Relation["MemberGroupMemberInfo.Group_Member_ID"] = "Member_Group_Member.Group_Member_ID";
            Relation["MemberGroupMemberInfo.Group_Member_GroupID"] = "Member_Group_Member.Group_Member_GroupID";
            Relation["MemberGroupMemberInfo.Group_Member_MemberID"] = "Member_Group_Member.Group_Member_MemberID";
            Relation["MemberGroupMemberInfo.Group_Member_AddTime"] = "Member_Group_Member.Group_Member_AddTime";
            Relation["MemberGroupMemberInfo.Group_Member_IsVisible"] = "Member_Group_Member.Group_Member_IsVisible";
            //Software_Cate
            Relation["SoftwareCateInfo.Software_Cate_ID"] = "Software_Cate.Software_Cate_ID";
            Relation["SoftwareCateInfo.Software_Cate_Name"] = "Software_Cate.Software_Cate_Name";
            Relation["SoftwareCateInfo.Software_Cate_IsActive"] = "Software_Cate.Software_Cate_IsActive";
            Relation["SoftwareCateInfo.Software_Cate_Sort"] = "Software_Cate.Software_Cate_Sort";
            Relation["SoftwareCateInfo.Software_Cate_SEO_Title"] = "Software_Cate.Software_Cate_SEO_Title";
            Relation["SoftwareCateInfo.Software_Cate_SEO_Keyword"] = "Software_Cate.Software_Cate_SEO_Keyword";
            Relation["SoftwareCateInfo.Software_Cate_SEO_Description"] = "Software_Cate.Software_Cate_SEO_Description";



            //Repair_Advice
            Relation["RepairAdviceInfo.Repair_Advice_ID"] = "Repair_Advice.Repair_Advice_ID";
            Relation["RepairAdviceInfo.Repair_Advice_MemberID"] = "Repair_Advice.Repair_Advice_MemberID";
            Relation["RepairAdviceInfo.Repair_Advice_DeviceName"] = "Repair_Advice.Repair_Advice_DeviceName";
            Relation["RepairAdviceInfo.Repair_Advice_Title"] = "Repair_Advice.Repair_Advice_Title";
            Relation["RepairAdviceInfo.Repair_Advice_Type"] = "Repair_Advice.Repair_Advice_Type";
            Relation["RepairAdviceInfo.Repair_Advice_Content"] = "Repair_Advice.Repair_Advice_Content";
            Relation["RepairAdviceInfo.Repair_Advice_Img"] = "Repair_Advice.Repair_Advice_Img";
            Relation["RepairAdviceInfo.Repair_Advice_ContactName"] = "Repair_Advice.Repair_Advice_ContactName";
            Relation["RepairAdviceInfo.Repair_Advice_ContactTel"] = "Repair_Advice.Repair_Advice_ContactTel";
            Relation["RepairAdviceInfo.Repair_Advice_AddTime"] = "Repair_Advice.Repair_Advice_AddTime";
            Relation["RepairAdviceInfo.Repair_Advice_Reply_IsRead"] = "Repair_Advice.Repair_Advice_Reply_IsRead";
            Relation["RepairAdviceInfo.Repair_Advice_Reply_Content"] = "Repair_Advice.Repair_Advice_Reply_Content";
            Relation["RepairAdviceInfo.Repair_Advice_Reply_AddTime"] = "Repair_Advice.Repair_Advice_Reply_AddTime";
            Relation["RepairAdviceInfo.Repair_Advice_Remarks"] = "Repair_Advice.Repair_Advice_Remarks";




            //Repair_Application
            Relation["RepairApplicationInfo.Repair_Application_ID"] = "Repair_Application.Repair_Application_ID";
            Relation["RepairApplicationInfo.Repair_Application_MemberID"] = "Repair_Application.Repair_Application_MemberID";
            Relation["RepairApplicationInfo.Repair_Application_DeviceID"] = "Repair_Application.Repair_Application_DeviceID";
            Relation["RepairApplicationInfo.Repair_Application_DeviceName"] = "Repair_Application.Repair_Application_DeviceName";
            Relation["RepairApplicationInfo.Repair_Application_SerialNumber"] = "Repair_Application.Repair_Application_SerialNumber";
            Relation["RepairApplicationInfo.Repair_Application_Trouble"] = "Repair_Application.Repair_Application_Trouble";
            Relation["RepairApplicationInfo.Repair_Application_RMA"] = "Repair_Application.Repair_Application_RMA";
            Relation["RepairApplicationInfo.Repair_Application_IsAudit"] = "Repair_Application.Repair_Application_IsAudit";
            Relation["RepairApplicationInfo.Repair_Application_CourierNumber"] = "Repair_Application.Repair_Application_CourierNumber";
            Relation["RepairApplicationInfo.Repair_Application_IsEvaluate"] = "Repair_Application.Repair_Application_IsEvaluate";
            Relation["RepairApplicationInfo.Repair_Application_Offer"] = "Repair_Application.Repair_Application_Offer";
            Relation["RepairApplicationInfo.Repair_Application_Solution"] = "Repair_Application.Repair_Application_Solution";
            Relation["RepairApplicationInfo.Repair_Application_IsMemberStatus"] = "Repair_Application.Repair_Application_IsMemberStatus";
            Relation["RepairApplicationInfo.Repair_Application_IsRepair"] = "Repair_Application.Repair_Application_IsRepair";
            Relation["RepairApplicationInfo.Repair_Application_ReturnNumber"] = "Repair_Application.Repair_Application_ReturnNumber";
            Relation["RepairApplicationInfo.Repair_Application_AddTime"] = "Repair_Application.Repair_Application_AddTime";
            Relation["RepairApplicationInfo.Repair_Application_Img"] = "Repair_Application.Repair_Application_Img";
            Relation["RepairApplicationInfo.Repair_Application_ExpectedOffer"] = "Repair_Application.Repair_Application_ExpectedOffer";
            Relation["RepairApplicationInfo.Repair_Application_Remark"] = "Repair_Application.Repair_Application_Remark";

            //LabelShop_Template_Private
            Relation["LabelShopTemplatePrivateInfo.ID"] = "LabelShop_Template_Private.ID";
            Relation["LabelShopTemplatePrivateInfo.ParentID"] = "LabelShop_Template_Private.ParentID";
            Relation["LabelShopTemplatePrivateInfo.CategoryID"] = "LabelShop_Template_Private.CategoryID";
            Relation["LabelShopTemplatePrivateInfo.Category"] = "LabelShop_Template_Private.Category";
            Relation["LabelShopTemplatePrivateInfo.Name"] = "LabelShop_Template_Private.Name";
            Relation["LabelShopTemplatePrivateInfo.ActiveStatus"] = "LabelShop_Template_Private.ActiveStatus";
            Relation["LabelShopTemplatePrivateInfo.Version"] = "LabelShop_Template_Private.Version";
            Relation["LabelShopTemplatePrivateInfo.fmtVesion"] = "LabelShop_Template_Private.fmtVesion";
            Relation["LabelShopTemplatePrivateInfo.ShareTime"] = "LabelShop_Template_Private.ShareTime";
            Relation["LabelShopTemplatePrivateInfo.MemberID"] = "LabelShop_Template_Private.MemberID";
            Relation["LabelShopTemplatePrivateInfo.DefaultPrinter"] = "LabelShop_Template_Private.DefaultPrinter";
            Relation["LabelShopTemplatePrivateInfo.LabelWidth"] = "LabelShop_Template_Private.LabelWidth";
            Relation["LabelShopTemplatePrivateInfo.LabelHeight"] = "LabelShop_Template_Private.LabelHeight";
            Relation["LabelShopTemplatePrivateInfo.PageType"] = "LabelShop_Template_Private.PageType";
            Relation["LabelShopTemplatePrivateInfo.PageWidth"] = "LabelShop_Template_Private.PageWidth";
            Relation["LabelShopTemplatePrivateInfo.PageHeight"] = "LabelShop_Template_Private.PageHeight";
            Relation["LabelShopTemplatePrivateInfo.Layout"] = "LabelShop_Template_Private.Layout";
            Relation["LabelShopTemplatePrivateInfo.Thumbnail"] = "LabelShop_Template_Private.Thumbnail";
            Relation["LabelShopTemplatePrivateInfo.XML"] = "LabelShop_Template_Private.XML";
            Relation["LabelShopTemplatePrivateInfo.CreateTime"] = "LabelShop_Template_Private.CreateTime";
            Relation["LabelShopTemplatePrivateInfo.UpdateTime"] = "LabelShop_Template_Private.UpdateTime";
            Relation["LabelShopTemplatePrivateInfo.LastVisitTime"] = "LabelShop_Template_Private.LastVisitTime";
            Relation["LabelShopTemplatePrivateInfo.LastPrintTime"] = "LabelShop_Template_Private.LastPrintTime";
            Relation["LabelShopTemplatePrivateInfo.PrintAmount"] = "LabelShop_Template_Private.PrintAmount";
            Relation["LabelShopTemplatePrivateInfo.VisitAmount"] = "LabelShop_Template_Private.VisitAmount";
            Relation["LabelShopTemplatePrivateInfo.UpdateAmount"] = "LabelShop_Template_Private.UpdateAmount";
            Relation["LabelShopTemplatePrivateInfo.Keywords"] = "LabelShop_Template_Private.Keywords";
            Relation["LabelShopTemplatePrivateInfo.Description"] = "LabelShop_Template_Private.Description";



            //LabelShop_Template_Shared
            Relation["LabelShopTemplateSharedInfo.ID"] = "LabelShop_Template_Shared.ID";
            Relation["LabelShopTemplateSharedInfo.ParentID"] = "LabelShop_Template_Shared.ParentID";
            Relation["LabelShopTemplateSharedInfo.CategoryID"] = "LabelShop_Template_Shared.CategoryID";
            Relation["LabelShopTemplateSharedInfo.Category"] = "LabelShop_Template_Shared.Category";
            Relation["LabelShopTemplateSharedInfo.Name"] = "LabelShop_Template_Shared.Name";
            Relation["LabelShopTemplateSharedInfo.ActiveStatus"] = "LabelShop_Template_Shared.ActiveStatus";
            Relation["LabelShopTemplateSharedInfo.Version"] = "LabelShop_Template_Shared.Version";
            Relation["LabelShopTemplateSharedInfo.fmtVesion"] = "LabelShop_Template_Shared.fmtVesion";
            Relation["LabelShopTemplateSharedInfo.ShareTime"] = "LabelShop_Template_Shared.ShareTime";
            Relation["LabelShopTemplateSharedInfo.MemberID"] = "LabelShop_Template_Shared.MemberID";
            Relation["LabelShopTemplateSharedInfo.DefaultPrinter"] = "LabelShop_Template_Shared.DefaultPrinter";
            Relation["LabelShopTemplateSharedInfo.LabelWidth"] = "LabelShop_Template_Shared.LabelWidth";
            Relation["LabelShopTemplateSharedInfo.LabelHeight"] = "LabelShop_Template_Shared.LabelHeight";
            Relation["LabelShopTemplateSharedInfo.PageType"] = "LabelShop_Template_Shared.PageType";
            Relation["LabelShopTemplateSharedInfo.PageWidth"] = "LabelShop_Template_Shared.PageWidth";
            Relation["LabelShopTemplateSharedInfo.PageHeight"] = "LabelShop_Template_Shared.PageHeight";
            Relation["LabelShopTemplateSharedInfo.Layout"] = "LabelShop_Template_Shared.Layout";
            Relation["LabelShopTemplateSharedInfo.Thumbnail"] = "LabelShop_Template_Shared.Thumbnail";
            Relation["LabelShopTemplateSharedInfo.XML"] = "LabelShop_Template_Shared.XML";
            Relation["LabelShopTemplateSharedInfo.CreateTime"] = "LabelShop_Template_Shared.CreateTime";
            Relation["LabelShopTemplateSharedInfo.UpdateTime"] = "LabelShop_Template_Shared.UpdateTime";
            Relation["LabelShopTemplateSharedInfo.LastVisitTime"] = "LabelShop_Template_Shared.LastVisitTime";
            Relation["LabelShopTemplateSharedInfo.LastPrintTime"] = "LabelShop_Template_Shared.LastPrintTime";
            Relation["LabelShopTemplateSharedInfo.PrintAmount"] = "LabelShop_Template_Shared.PrintAmount";
            Relation["LabelShopTemplateSharedInfo.VisitAmount"] = "LabelShop_Template_Shared.VisitAmount";
            Relation["LabelShopTemplateSharedInfo.UpdateAmount"] = "LabelShop_Template_Shared.UpdateAmount";
            Relation["LabelShopTemplateSharedInfo.Keywords"] = "LabelShop_Template_Shared.Keywords";
            Relation["LabelShopTemplateSharedInfo.Description"] = "LabelShop_Template_Shared.Description";
            Relation["LabelShopTemplateSharedInfo.GroupID"] = "LabelShop_Template_Shared.GroupID";
            Relation["LabelShopTemplateSharedInfo.AuditStatus"] = "LabelShop_Template_Shared.AuditStatus";


            //LabelShop_Template_Deleted
            Relation["LabelShopTemplateDeletedInfo.ID"] = "LabelShop_Template_Deleted.ID";
            Relation["LabelShopTemplateDeletedInfo.ParentID"] = "LabelShop_Template_Deleted.ParentID";
            Relation["LabelShopTemplateDeletedInfo.CategoryID"] = "LabelShop_Template_Deleted.CategoryID";
            Relation["LabelShopTemplateDeletedInfo.Category"] = "LabelShop_Template_Deleted.Category";
            Relation["LabelShopTemplateDeletedInfo.Name"] = "LabelShop_Template_Deleted.Name";
            Relation["LabelShopTemplateDeletedInfo.ActiveStatus"] = "LabelShop_Template_Deleted.ActiveStatus";
            Relation["LabelShopTemplateDeletedInfo.Version"] = "LabelShop_Template_Deleted.Version";
            Relation["LabelShopTemplateDeletedInfo.fmtVesion"] = "LabelShop_Template_Deleted.fmtVesion";
            Relation["LabelShopTemplateDeletedInfo.ShareTime"] = "LabelShop_Template_Deleted.ShareTime";
            Relation["LabelShopTemplateDeletedInfo.MemberID"] = "LabelShop_Template_Deleted.MemberID";
            Relation["LabelShopTemplateDeletedInfo.DefaultPrinter"] = "LabelShop_Template_Deleted.DefaultPrinter";
            Relation["LabelShopTemplateDeletedInfo.LabelWidth"] = "LabelShop_Template_Deleted.LabelWidth";
            Relation["LabelShopTemplateDeletedInfo.LabelHeight"] = "LabelShop_Template_Deleted.LabelHeight";
            Relation["LabelShopTemplateDeletedInfo.PageType"] = "LabelShop_Template_Deleted.PageType";
            Relation["LabelShopTemplateDeletedInfo.PageWidth"] = "LabelShop_Template_Deleted.PageWidth";
            Relation["LabelShopTemplateDeletedInfo.PageHeight"] = "LabelShop_Template_Deleted.PageHeight";
            Relation["LabelShopTemplateDeletedInfo.Layout"] = "LabelShop_Template_Deleted.Layout";
            Relation["LabelShopTemplateDeletedInfo.Thumbnail"] = "LabelShop_Template_Deleted.Thumbnail";
            Relation["LabelShopTemplateDeletedInfo.XML"] = "LabelShop_Template_Deleted.XML";
            Relation["LabelShopTemplateDeletedInfo.CreateTime"] = "LabelShop_Template_Deleted.CreateTime";
            Relation["LabelShopTemplateDeletedInfo.UpdateTime"] = "LabelShop_Template_Deleted.UpdateTime";
            Relation["LabelShopTemplateDeletedInfo.LastVisitTime"] = "LabelShop_Template_Deleted.LastVisitTime";
            Relation["LabelShopTemplateDeletedInfo.LastPrintTime"] = "LabelShop_Template_Deleted.LastPrintTime";
            Relation["LabelShopTemplateDeletedInfo.PrintAmount"] = "LabelShop_Template_Deleted.PrintAmount";
            Relation["LabelShopTemplateDeletedInfo.VisitAmount"] = "LabelShop_Template_Deleted.VisitAmount";
            Relation["LabelShopTemplateDeletedInfo.UpdateAmount"] = "LabelShop_Template_Deleted.UpdateAmount";
            Relation["LabelShopTemplateDeletedInfo.Keywords"] = "LabelShop_Template_Deleted.Keywords";
            Relation["LabelShopTemplateDeletedInfo.Description"] = "LabelShop_Template_Deleted.Description";
            Relation["LabelShopTemplateDeletedInfo.GroupID"] = "LabelShop_Template_Deleted.GroupID";
            Relation["LabelShopTemplateDeletedInfo.DeleteTIme"] = "LabelShop_Template_Deleted.DeleteTIme";




            //LabelShop_License_InUse
            Relation["LabelShopLicenseInUseInfo.SN"] = "LabelShop_License_InUse.SN";
            Relation["LabelShopLicenseInUseInfo.CreateTime"] = "LabelShop_License_InUse.CreateTime";
            Relation["LabelShopLicenseInUseInfo.IsUsed"] = "LabelShop_License_InUse.IsUsed";
            Relation["LabelShopLicenseInUseInfo.MemberID"] = "LabelShop_License_InUse.MemberID";
            Relation["LabelShopLicenseInUseInfo.SID"] = "LabelShop_License_InUse.SID";
            Relation["LabelShopLicenseInUseInfo.MID"] = "LabelShop_License_InUse.MID";
            Relation["LabelShopLicenseInUseInfo.AuthorizeCode"] = "LabelShop_License_InUse.AuthorizeCode";
            Relation["LabelShopLicenseInUseInfo.AuthorizeType"] = "LabelShop_License_InUse.AuthorizeType";
            Relation["LabelShopLicenseInUseInfo.AuthorizeTime"] = "LabelShop_License_InUse.AuthorizeTime";
            Relation["LabelShopLicenseInUseInfo.ExpiredTime"] = "LabelShop_License_InUse.ExpiredTime";
            Relation["LabelShopLicenseInUseInfo.LastValidateTime"] = "LabelShop_License_InUse.LastValidateTime";
            Relation["LabelShopLicenseInUseInfo.ValidateAmount"] = "LabelShop_License_InUse.ValidateAmount";


            //LabelShop_License_Expired
            Relation["LabelShopLicenseExpiredInfo.SN"] = "LabelShop_License_Expired.SN";
            Relation["LabelShopLicenseExpiredInfo.CreateTime"] = "LabelShop_License_Expired.CreateTime";
            Relation["LabelShopLicenseExpiredInfo.IsUsed"] = "LabelShop_License_Expired.IsUsed";
            Relation["LabelShopLicenseExpiredInfo.MemberID"] = "LabelShop_License_Expired.MemberID";
            Relation["LabelShopLicenseExpiredInfo.SID"] = "LabelShop_License_Expired.SID";
            Relation["LabelShopLicenseExpiredInfo.MID"] = "LabelShop_License_Expired.MID";
            Relation["LabelShopLicenseExpiredInfo.AuthorizeCode"] = "LabelShop_License_Expired.AuthorizeCode";
            Relation["LabelShopLicenseExpiredInfo.AuthorizeType"] = "LabelShop_License_Expired.AuthorizeType";
            Relation["LabelShopLicenseExpiredInfo.AuthorizeTime"] = "LabelShop_License_Expired.AuthorizeTime";
            Relation["LabelShopLicenseExpiredInfo.ExpiredTime"] = "LabelShop_License_Expired.ExpiredTime";
            Relation["LabelShopLicenseExpiredInfo.LastValidateTime"] = "LabelShop_License_Expired.LastValidateTime";
            Relation["LabelShopLicenseExpiredInfo.ValidateAmount"] = "LabelShop_License_Expired.ValidateAmount";


            //LabelShop_License_UnUsed
            Relation["LabelShopLicenseUnUsedInfo.ID"] = "LabelShop_License_UnUsed.ID";
            Relation["LabelShopLicenseUnUsedInfo.Order_SN"] = "LabelShop_License_UnUsed.Order_SN";
            Relation["LabelShopLicenseUnUsedInfo.SN"] = "LabelShop_License_UnUsed.SN";
            Relation["LabelShopLicenseUnUsedInfo.CreateTime"] = "LabelShop_License_UnUsed.CreateTime";
            Relation["LabelShopLicenseUnUsedInfo.IsUsed"] = "LabelShop_License_UnUsed.IsUsed";
            Relation["LabelShopLicenseUnUsedInfo.MemberID"] = "LabelShop_License_UnUsed.MemberID";
            Relation["LabelShopLicenseUnUsedInfo.SID"] = "LabelShop_License_UnUsed.SID";
            Relation["LabelShopLicenseUnUsedInfo.MID"] = "LabelShop_License_UnUsed.MID";
            Relation["LabelShopLicenseUnUsedInfo.AuthorizeCode"] = "LabelShop_License_UnUsed.AuthorizeCode";
            Relation["LabelShopLicenseUnUsedInfo.AuthorizeType"] = "LabelShop_License_UnUsed.AuthorizeType";
            Relation["LabelShopLicenseUnUsedInfo.AuthorizeTime"] = "LabelShop_License_UnUsed.AuthorizeTime";
            Relation["LabelShopLicenseUnUsedInfo.ExpiredTime"] = "LabelShop_License_UnUsed.ExpiredTime";
            Relation["LabelShopLicenseUnUsedInfo.LastValidateTime"] = "LabelShop_License_UnUsed.LastValidateTime";
            Relation["LabelShopLicenseUnUsedInfo.ValidateAmount"] = "LabelShop_License_UnUsed.ValidateAmount";


            //LabelShop_License_Orders
            Relation["LabelShopLicenseOrdersInfo.ID"] = "LabelShop_License_Orders.ID";
            Relation["LabelShopLicenseOrdersInfo.SN"] = "LabelShop_License_Orders.SN";
            Relation["LabelShopLicenseOrdersInfo.MemberID"] = "LabelShop_License_Orders.MemberID";
            Relation["LabelShopLicenseOrdersInfo.SID"] = "LabelShop_License_Orders.SID";
            Relation["LabelShopLicenseOrdersInfo.SName"] = "LabelShop_License_Orders.SName";
            Relation["LabelShopLicenseOrdersInfo.BuyDate"] = "LabelShop_License_Orders.BuyDate";
            Relation["LabelShopLicenseOrdersInfo.MID"] = "LabelShop_License_Orders.MID";
            Relation["LabelShopLicenseOrdersInfo.AuthorizeType"] = "LabelShop_License_Orders.AuthorizeType";
            Relation["LabelShopLicenseOrdersInfo.CreateTime"] = "LabelShop_License_Orders.CreateTime";
            Relation["LabelShopLicenseOrdersInfo.TotalPrice"] = "LabelShop_License_Orders.TotalPrice";
            Relation["LabelShopLicenseOrdersInfo.ProductCode"] = "LabelShop_License_Orders.ProductCode";
            Relation["LabelShopLicenseOrdersInfo.Payment_Status"] = "LabelShop_License_Orders.Payment_Status";
            Relation["LabelShopLicenseOrdersInfo.Payment_Time"] = "LabelShop_License_Orders.Payment_Time";
            Relation["LabelShopLicenseOrdersInfo.Status"] = "LabelShop_License_Orders.Status";
            Relation["LabelShopLicenseOrdersInfo.Amount"] = "LabelShop_License_Orders.Amount";
            Relation["LabelShopLicenseOrdersInfo.UnitPrice"] = "LabelShop_License_Orders.UnitPrice";
            Relation["LabelShopLicenseOrdersInfo.FailTime"] = "LabelShop_License_Orders.FailTime";
            Relation["LabelShopLicenseOrdersInfo.FailNote"] = "LabelShop_License_Orders.FailNote";
            Relation["LabelShopLicenseOrdersInfo.FailUserID"] = "LabelShop_License_Orders.FailUserID";
            Relation["LabelShopLicenseOrdersInfo.Orders_Note"] = "LabelShop_License_Orders.Orders_Note";
            Relation["LabelShopLicenseOrdersInfo.Orders_Admin_Note"] = "LabelShop_License_Orders.Orders_Admin_Note";
            Relation["LabelShopLicenseOrdersInfo.Orders_Payway"] = "LabelShop_License_Orders.Orders_Payway";
            Relation["LabelShopLicenseOrdersInfo.Orders_Payway_Name"] = "LabelShop_License_Orders.Orders_Payway_Name";
            Relation["LabelShopLicenseOrdersInfo.SVersion"] = "LabelShop_License_Orders.SVersion";
            Relation["LabelShopLicenseOrdersInfo.Orders_Coin"] = "LabelShop_License_Orders.Orders_Coin";
            Relation["LabelShopLicenseOrdersInfo.Orders_IsReturnCoin"] = "LabelShop_License_Orders.Orders_IsReturnCoin";
            Relation["LabelShopLicenseOrdersInfo.IsSettlement"] = "LabelShop_License_Orders.IsSettlement";
            Relation["LabelShopLicenseOrdersInfo.Recovery"] = "LabelShop_License_Orders.Recovery";


            //Print_Order_RelateExtend
            Relation["PrintOrderRelateExtendInfo.Print_Order_RelateExtend_ID"] = "Print_Order_RelateExtend.Print_Order_RelateExtend_ID";
            Relation["PrintOrderRelateExtendInfo.Print_Order_ID"] = "Print_Order_RelateExtend.Print_Order_ID";
            Relation["PrintOrderRelateExtendInfo.Print_Order_ExtendName"] = "Print_Order_RelateExtend.Print_Order_ExtendName";
            Relation["PrintOrderRelateExtendInfo.Print_Order_Extend_Val"] = "Print_Order_RelateExtend.Print_Order_Extend_Val";



            //Print_Order_Log
            Relation["PrintOrderLogInfo.Print_Order_Log_ID"] = "Print_Order_Log.Print_Order_Log_ID";
            Relation["PrintOrderLogInfo.Print_Order_Log_OrdersID"] = "Print_Order_Log.Print_Order_Log_OrdersID";
            Relation["PrintOrderLogInfo.Print_Order_Log_Addtime"] = "Print_Order_Log.Print_Order_Log_Addtime";
            Relation["PrintOrderLogInfo.Print_Order_Log_Operator"] = "Print_Order_Log.Print_Order_Log_Operator";
            Relation["PrintOrderLogInfo.Print_Order_Log_Remark"] = "Print_Order_Log.Print_Order_Log_Remark";
            Relation["PrintOrderLogInfo.Print_Order_Log_Action"] = "Print_Order_Log.Print_Order_Log_Action";
            Relation["PrintOrderLogInfo.Print_Order_Log_Result"] = "Print_Order_Log.Print_Order_Log_Result";



            //Print_Order
            Relation["PrintOrderInfo.Print_Order_ID"] = "Print_Order.Print_Order_ID";
            Relation["PrintOrderInfo.Print_Order_SN"] = "Print_Order.Print_Order_SN";
            Relation["PrintOrderInfo.Print_Order_MemberID"] = "Print_Order.Print_Order_MemberID";
            Relation["PrintOrderInfo.Print_Order_CreateTime"] = "Print_Order.Print_Order_CreateTime";
            Relation["PrintOrderInfo.Print_Order_ReceiptTime"] = "Print_Order.Print_Order_ReceiptTime";
            Relation["PrintOrderInfo.Print_Order_Status"] = "Print_Order.Print_Order_Status";
            Relation["PrintOrderInfo.Print_Order_Payment_Time"] = "Print_Order.Print_Order_Payment_Time";
            Relation["PrintOrderInfo.Print_Order_Payment_Status"] = "Print_Order.Print_Order_Payment_Status";
            Relation["PrintOrderInfo.Print_Order_ServicesName"] = "Print_Order.Print_Order_ServicesName";
            Relation["PrintOrderInfo.Print_Order_Type"] = "Print_Order.Print_Order_Type";
            Relation["PrintOrderInfo.Print_Order_TagType"] = "Print_Order.Print_Order_TagType";
            Relation["PrintOrderInfo.Print_Order_Material"] = "Print_Order.Print_Order_Material";
            Relation["PrintOrderInfo.Print_Order_LabelNumber"] = "Print_Order.Print_Order_LabelNumber";
            Relation["PrintOrderInfo.Print_Order_AxisDiameter"] = "Print_Order.Print_Order_AxisDiameter";
            Relation["PrintOrderInfo.Print_Order_ServiceAttr"] = "Print_Order.Print_Order_ServiceAttr";
            Relation["PrintOrderInfo.Print_Order_Colour"] = "Print_Order.Print_Order_Colour";
            Relation["PrintOrderInfo.Print_Order_width"] = "Print_Order.Print_Order_width";
            Relation["PrintOrderInfo.Print_Order_heigth"] = "Print_Order.Print_Order_heigth";
            Relation["PrintOrderInfo.Print_Order_TagNumber"] = "Print_Order.Print_Order_TagNumber";
            Relation["PrintOrderInfo.Print_Order_Price"] = "Print_Order.Print_Order_Price";
            Relation["PrintOrderInfo.Print_Order_IsUrgent"] = "Print_Order.Print_Order_IsUrgent";
            Relation["PrintOrderInfo.Print_Order_Day"] = "Print_Order.Print_Order_Day";
            Relation["PrintOrderInfo.Print_Order_Annex"] = "Print_Order.Print_Order_Annex";
            Relation["PrintOrderInfo.Print_Order_Freight"] = "Print_Order.Print_Order_Freight";
            Relation["PrintOrderInfo.Print_Order_Total"] = "Print_Order.Print_Order_Total";
            Relation["PrintOrderInfo.Print_Order_amount"] = "Print_Order.Print_Order_amount";
            Relation["PrintOrderInfo.Print_Order_Remark"] = "Print_Order.Print_Order_Remark";
            Relation["PrintOrderInfo.Print_Order_LabelShopID"] = "Print_Order.Print_Order_LabelShopID";
            Relation["PrintOrderInfo.Print_Order_ParentID"] = "Print_Order.Print_Order_ParentID";
            Relation["PrintOrderInfo.Print_Order_Category"] = "Print_Order.Print_Order_Category";
            Relation["PrintOrderInfo.Print_Order_Name"] = "Print_Order.Print_Order_Name";
            Relation["PrintOrderInfo.Print_Order_ActiveStatus"] = "Print_Order.Print_Order_ActiveStatus";
            Relation["PrintOrderInfo.Print_Order_Version"] = "Print_Order.Print_Order_Version";
            Relation["PrintOrderInfo.Print_Order_fmtVesion"] = "Print_Order.Print_Order_fmtVesion";
            Relation["PrintOrderInfo.Print_Order_DefaultPrinter"] = "Print_Order.Print_Order_DefaultPrinter";
            Relation["PrintOrderInfo.Print_Order_LabelWidth"] = "Print_Order.Print_Order_LabelWidth";
            Relation["PrintOrderInfo.Print_Order_LabelHeight"] = "Print_Order.Print_Order_LabelHeight";
            Relation["PrintOrderInfo.Print_Order_PageType"] = "Print_Order.Print_Order_PageType";
            Relation["PrintOrderInfo.Print_Order_PageWidth"] = "Print_Order.Print_Order_PageWidth";
            Relation["PrintOrderInfo.Print_Order_PageHeight"] = "Print_Order.Print_Order_PageHeight";
            Relation["PrintOrderInfo.Print_Order_Layout"] = "Print_Order.Print_Order_Layout";
            Relation["PrintOrderInfo.Print_Order_Thumbnail"] = "Print_Order.Print_Order_Thumbnail";
            Relation["PrintOrderInfo.Print_Order_XML"] = "Print_Order.Print_Order_XML";
            Relation["PrintOrderInfo.Print_Order_Description"] = "Print_Order.Print_Order_Description";
            Relation["PrintOrderInfo.Print_DeliveryStatus"] = "Print_Order.Print_DeliveryStatus";
            Relation["PrintOrderInfo.Print_DeliveryStatus_Time"] = "Print_Order.Print_DeliveryStatus_Time";
            Relation["PrintOrderInfo.Print_InvoiceStatus"] = "Print_Order.Print_InvoiceStatus";
            Relation["PrintOrderInfo.Print_Fail_SysUserID"] = "Print_Order.Print_Fail_SysUserID";
            Relation["PrintOrderInfo.Print_Fail_Note"] = "Print_Order.Print_Fail_Note";
            Relation["PrintOrderInfo.Print_Fail_Addtime"] = "Print_Order.Print_Fail_Addtime";
            Relation["PrintOrderInfo.Print_Address_ID"] = "Print_Order.Print_Address_ID";
            Relation["PrintOrderInfo.Print_Address_Country"] = "Print_Order.Print_Address_Country";
            Relation["PrintOrderInfo.Print_Address_State"] = "Print_Order.Print_Address_State";
            Relation["PrintOrderInfo.Print_Address_City"] = "Print_Order.Print_Address_City";
            Relation["PrintOrderInfo.Print_Address_County"] = "Print_Order.Print_Address_County";
            Relation["PrintOrderInfo.Print_Address_StreetAddress"] = "Print_Order.Print_Address_StreetAddress";
            Relation["PrintOrderInfo.Print_Address_Zip"] = "Print_Order.Print_Address_Zip";
            Relation["PrintOrderInfo.Print_Address_Name"] = "Print_Order.Print_Address_Name";
            Relation["PrintOrderInfo.Print_Address_Phone_Countrycode"] = "Print_Order.Print_Address_Phone_Countrycode";
            Relation["PrintOrderInfo.Print_Address_Phone_Areacode"] = "Print_Order.Print_Address_Phone_Areacode";
            Relation["PrintOrderInfo.Print_Address_Phone_Number"] = "Print_Order.Print_Address_Phone_Number";
            Relation["PrintOrderInfo.Print_Address_Mobile"] = "Print_Order.Print_Address_Mobile";
            Relation["PrintOrderInfo.Orders_Delivery_Time_ID"] = "Print_Order.Orders_Delivery_Time_ID";
            Relation["PrintOrderInfo.Orders_Delivery"] = "Print_Order.Orders_Delivery";
            Relation["PrintOrderInfo.Orders_Delivery_Name"] = "Print_Order.Orders_Delivery_Name";
            Relation["PrintOrderInfo.Orders_Payway"] = "Print_Order.Orders_Payway";
            Relation["PrintOrderInfo.Orders_Payway_Name"] = "Print_Order.Orders_Payway_Name";
            Relation["PrintOrderInfo.Print_Site"] = "Print_Order.Print_Site";
            Relation["PrintOrderInfo.Print_Order_PrintStatus"] = "Print_Order.Print_Order_PrintStatus";
            Relation["PrintOrderInfo.Print_Order_PrintAmount"] = "Print_Order.Print_Order_PrintAmount";
            Relation["PrintOrderInfo.Print_Order_ProductID"] = "Print_Order.Print_Order_ProductID";
            Relation["PrintOrderInfo.Print_Order_PrintPrice"] = "Print_Order.Print_Order_PrintPrice";
            Relation["PrintOrderInfo.Print_Order_ProductName"] = "Print_Order.Print_Order_ProductName";
            Relation["PrintOrderInfo.Print_Orderl_Coin"] = "Print_Order.Print_Orderl_Coin";
            Relation["PrintOrderInfo.Print_Orderl_IsReturnCoin"] = "Print_Order.Print_Orderl_IsReturnCoin";
            Relation["PrintOrderInfo.Print_Order_IsWaste"] = "Print_Order.Print_Order_IsWaste";
            Relation["PrintOrderInfo.Print_Order_Finished"] = "Print_Order.Print_Order_Finished";
            Relation["PrintOrderInfo.Print_Order_LineNumber"] = "Print_Order.Print_Order_LineNumber";
            Relation["PrintOrderInfo.Print_Order_LabelCount"] = "Print_Order.Print_Order_LabelCount";
            Relation["PrintOrderInfo.Print_Order_ManualSetup"] = "Print_Order.Print_Order_ManualSetup";
            Relation["PrintOrderInfo.Print_Order_IsInt"] = "Print_Order.Print_Order_IsInt";
            Relation["PrintOrderInfo.Print_Order_RealAmount"] = "Print_Order.Print_Order_RealAmount";
            Relation["PrintOrderInfo.Print_Order_RealRemarks"] = "Print_Order.Print_Order_RealRemarks";
            Relation["PrintOrderInfo.Print_Order_Printer"] = "Print_Order.Print_Order_Printer";
            Relation["PrintOrderInfo.Print_Order_LabelInterval"] = "Print_Order.Print_Order_LabelInterval";
            Relation["PrintOrderInfo.Print_Order_Total_PriceDiscount"] = "Print_Order.Print_Order_Total_PriceDiscount";
            Relation["PrintOrderInfo.Print_Order_Total_PriceDiscount_Note"] = "Print_Order.Print_Order_Total_PriceDiscount_Note";
            Relation["PrintOrderInfo.Print_Order_Finished_Extend"] = "Print_Order.Print_Order_Finished_Extend";
            Relation["PrintOrderInfo.Print_Order_IsSettlement"] = "Print_Order.Print_Order_IsSettlement";
            Relation["PrintOrderInfo.Print_Order_Admin_Note"] = "Print_Order.Print_Order_Admin_Note";
            Relation["PrintOrderInfo.Print_Order_Recovery"] = "Print_Order.Print_Order_Recovery";
            Relation["PrintOrderInfo.Print_Orders_InvoiceEmail"] = "Print_Order.Print_Orders_InvoiceEmail";


            //LabelShop_License_Orders_Log
            Relation["LabelShopLicenseOrdersLogInfo.Orders_Log_ID"] = "LabelShop_License_Orders_Log.Orders_Log_ID";
            Relation["LabelShopLicenseOrdersLogInfo.Orders_Log_OrdersID"] = "LabelShop_License_Orders_Log.Orders_Log_OrdersID";
            Relation["LabelShopLicenseOrdersLogInfo.Orders_Log_Addtime"] = "LabelShop_License_Orders_Log.Orders_Log_Addtime";
            Relation["LabelShopLicenseOrdersLogInfo.Orders_Log_Operator"] = "LabelShop_License_Orders_Log.Orders_Log_Operator";
            Relation["LabelShopLicenseOrdersLogInfo.Orders_Log_Remark"] = "LabelShop_License_Orders_Log.Orders_Log_Remark";
            Relation["LabelShopLicenseOrdersLogInfo.Orders_Log_Action"] = "LabelShop_License_Orders_Log.Orders_Log_Action";
            Relation["LabelShopLicenseOrdersLogInfo.Orders_Log_Result"] = "LabelShop_License_Orders_Log.Orders_Log_Result";

            //LabelShop_License_Orders_Payment
            Relation["LabelShopLicenseOrdersPaymentInfo.Orders_Payment_ID"] = "LabelShop_License_Orders_Payment.Orders_Payment_ID";
            Relation["LabelShopLicenseOrdersPaymentInfo.Orders_Payment_OrdersID"] = "LabelShop_License_Orders_Payment.Orders_Payment_OrdersID";
            Relation["LabelShopLicenseOrdersPaymentInfo.Orders_Payment_PaymentStatus"] = "LabelShop_License_Orders_Payment.Orders_Payment_PaymentStatus";
            Relation["LabelShopLicenseOrdersPaymentInfo.Orders_Payment_SysUserID"] = "LabelShop_License_Orders_Payment.Orders_Payment_SysUserID";
            Relation["LabelShopLicenseOrdersPaymentInfo.Orders_Payment_DocNo"] = "LabelShop_License_Orders_Payment.Orders_Payment_DocNo";
            Relation["LabelShopLicenseOrdersPaymentInfo.Orders_Payment_Name"] = "LabelShop_License_Orders_Payment.Orders_Payment_Name";
            Relation["LabelShopLicenseOrdersPaymentInfo.Orders_Payment_Amount"] = "LabelShop_License_Orders_Payment.Orders_Payment_Amount";
            Relation["LabelShopLicenseOrdersPaymentInfo.Orders_Payment_Note"] = "LabelShop_License_Orders_Payment.Orders_Payment_Note";
            Relation["LabelShopLicenseOrdersPaymentInfo.Orders_Payment_Addtime"] = "LabelShop_License_Orders_Payment.Orders_Payment_Addtime";
            Relation["LabelShopLicenseOrdersPaymentInfo.Orders_Payment_Site"] = "LabelShop_License_Orders_Payment.Orders_Payment_Site";



            //Print_Delivery
            Relation["PrintDeliveryInfo.Print_Delivery_ID"] = "Print_Delivery.Print_Delivery_ID";
            Relation["PrintDeliveryInfo.Print_Delivery_PrintID"] = "Print_Delivery.Print_Delivery_PrintID";
            Relation["PrintDeliveryInfo.Print_Delivery_DeliveryStatus"] = "Print_Delivery.Print_Delivery_DeliveryStatus";
            Relation["PrintDeliveryInfo.Print_Delivery_SysUserID"] = "Print_Delivery.Print_Delivery_SysUserID";
            Relation["PrintDeliveryInfo.Print_Delivery_DocNo"] = "Print_Delivery.Print_Delivery_DocNo";
            Relation["PrintDeliveryInfo.Print_Delivery_Name"] = "Print_Delivery.Print_Delivery_Name";
            Relation["PrintDeliveryInfo.Print_Delivery_companyName"] = "Print_Delivery.Print_Delivery_companyName";
            Relation["PrintDeliveryInfo.Print_Delivery_Code"] = "Print_Delivery.Print_Delivery_Code";
            Relation["PrintDeliveryInfo.Print_Delivery_Amount"] = "Print_Delivery.Print_Delivery_Amount";
            Relation["PrintDeliveryInfo.Print_Delivery_Note"] = "Print_Delivery.Print_Delivery_Note";
            Relation["PrintDeliveryInfo.Print_Delivery_Addtime"] = "Print_Delivery.Print_Delivery_Addtime";
            Relation["PrintDeliveryInfo.Print_Delivery_Site"] = "Print_Delivery.Print_Delivery_Site";


            //Print_Invoice
            Relation["PrintInvoiceInfo.Invoice_ID"] = "Print_Invoice.Invoice_ID";
            Relation["PrintInvoiceInfo.Invoice_PrintID"] = "Print_Invoice.Invoice_PrintID";
            Relation["PrintInvoiceInfo.Invoice_Type"] = "Print_Invoice.Invoice_Type";
            Relation["PrintInvoiceInfo.Invoice_Title"] = "Print_Invoice.Invoice_Title";
            Relation["PrintInvoiceInfo.Invoice_Content"] = "Print_Invoice.Invoice_Content";
            Relation["PrintInvoiceInfo.Invoice_FirmName"] = "Print_Invoice.Invoice_FirmName";
            Relation["PrintInvoiceInfo.Invoice_VAT_FirmName"] = "Print_Invoice.Invoice_VAT_FirmName";
            Relation["PrintInvoiceInfo.Invoice_VAT_Code"] = "Print_Invoice.Invoice_VAT_Code";
            Relation["PrintInvoiceInfo.Invoice_VAT_RegAddr"] = "Print_Invoice.Invoice_VAT_RegAddr";
            Relation["PrintInvoiceInfo.Invoice_VAT_RegTel"] = "Print_Invoice.Invoice_VAT_RegTel";
            Relation["PrintInvoiceInfo.Invoice_VAT_Bank"] = "Print_Invoice.Invoice_VAT_Bank";
            Relation["PrintInvoiceInfo.Invoice_VAT_BankAcount"] = "Print_Invoice.Invoice_VAT_BankAcount";
            Relation["PrintInvoiceInfo.Invoice_VAT_Content"] = "Print_Invoice.Invoice_VAT_Content";
            Relation["PrintInvoiceInfo.Invoice_VAT_Annex"] = "Print_Invoice.Invoice_VAT_Annex";

            //Print_Payment
            Relation["PrintPaymentInfo.Print_Payment_ID"] = "Print_Payment.Print_Payment_ID";
            Relation["PrintPaymentInfo.Print_Payment_PrintID"] = "Print_Payment.Print_Payment_PrintID";
            Relation["PrintPaymentInfo.Print_Payment_PaymentStatus"] = "Print_Payment.Print_Payment_PaymentStatus";
            Relation["PrintPaymentInfo.Print_Payment_SysUserID"] = "Print_Payment.Print_Payment_SysUserID";
            Relation["PrintPaymentInfo.Print_Payment_DocNo"] = "Print_Payment.Print_Payment_DocNo";
            Relation["PrintPaymentInfo.Print_Payment_Name"] = "Print_Payment.Print_Payment_Name";
            Relation["PrintPaymentInfo.Print_Payment_Amount"] = "Print_Payment.Print_Payment_Amount";
            Relation["PrintPaymentInfo.Print_Payment_Note"] = "Print_Payment.Print_Payment_Note";
            Relation["PrintPaymentInfo.Print_Payment_Addtime"] = "Print_Payment.Print_Payment_Addtime";
            Relation["PrintPaymentInfo.Print_Payment_Site"] = "Print_Payment.Print_Payment_Site";


            //LabelShop_Cate
            Relation["LabelShopCateInfo.LabelShop_Cate_ID"] = "LabelShop_Cate.LabelShop_Cate_ID";
            Relation["LabelShopCateInfo.LabelShop_Cate_ParentID"] = "LabelShop_Cate.LabelShop_Cate_ParentID";
            Relation["LabelShopCateInfo.LabelShop_Cate_Name"] = "LabelShop_Cate.LabelShop_Cate_Name";
            Relation["LabelShopCateInfo.LabelShop_Cate_Sort"] = "LabelShop_Cate.LabelShop_Cate_Sort";
            Relation["LabelShopCateInfo.LabelShop_Cate_IsActive"] = "LabelShop_Cate.LabelShop_Cate_IsActive";



            //Print_Config
            Relation["PrintConfigInfo.Print_Config_ID"] = "Print_Config.Print_Config_ID";
            Relation["PrintConfigInfo.Print_Config_Material"] = "Print_Config.Print_Config_Material";
            Relation["PrintConfigInfo.Print_Config_LabelNumber"] = "Print_Config.Print_Config_LabelNumber";
            Relation["PrintConfigInfo.Print_Config_AxisDiameter"] = "Print_Config.Print_Config_AxisDiameter";
            Relation["PrintConfigInfo.Print_Config_Homochromy"] = "Print_Config.Print_Config_Homochromy";
            Relation["PrintConfigInfo.Print_Config_Multicolor"] = "Print_Config.Print_Config_Multicolor";
            Relation["PrintConfigInfo.Print_Config_PrintPrice"] = "Print_Config.Print_Config_PrintPrice";
            Relation["PrintConfigInfo.Print_Config_PrintingPrice"] = "Print_Config.Print_Config_PrintingPrice";
            Relation["PrintConfigInfo.Print_Config_DieCuttingPrice"] = "Print_Config.Print_Config_DieCuttingPrice";
            Relation["PrintConfigInfo.Print_Config_UrgentPrice"] = "Print_Config.Print_Config_UrgentPrice";
            Relation["PrintConfigInfo.Print_Config_UrgentCoefficient"] = "Print_Config.Print_Config_UrgentCoefficient";
            Relation["PrintConfigInfo.Print_Config_PrintingCoefficient"] = "Print_Config.Print_Config_PrintingCoefficient";
            Relation["PrintConfigInfo.Print_Config_TopCate"] = "Print_Config.Print_Config_TopCate";
            Relation["PrintConfigInfo.Print_Config_LabelInterval"] = "Print_Config.Print_Config_LabelInterval";
            Relation["PrintConfigInfo.Print_Config_ProcessName1"] = "Print_Config.Print_Config_ProcessName1";
            Relation["PrintConfigInfo.Print_Config_ProcessRemark1"] = "Print_Config.Print_Config_ProcessRemark1";
            Relation["PrintConfigInfo.Print_Config_ProcessName2"] = "Print_Config.Print_Config_ProcessName2";
            Relation["PrintConfigInfo.Print_Config_ProcessRemark2"] = "Print_Config.Print_Config_ProcessRemark2";
            Relation["PrintConfigInfo.Print_Config_ProcessName3"] = "Print_Config.Print_Config_ProcessName3";
            Relation["PrintConfigInfo.Print_Config_ProcessRemark3"] = "Print_Config.Print_Config_ProcessRemark3";
            Relation["PrintConfigInfo.Print_Config_ProcessName4"] = "Print_Config.Print_Config_ProcessName4";
            Relation["PrintConfigInfo.Print_Config_ProcessRemark4"] = "Print_Config.Print_Config_ProcessRemark4";
            Relation["PrintConfigInfo.Print_Config_img1"] = "Print_Config.Print_Config_img1";
            Relation["PrintConfigInfo.Print_Config_img2"] = "Print_Config.Print_Config_img2";
            Relation["PrintConfigInfo.Print_Config_img3"] = "Print_Config.Print_Config_img3";
            Relation["PrintConfigInfo.Print_Config_img4"] = "Print_Config.Print_Config_img4";

            //Print_Amount
            Relation["PrintAmountInfo.Print_Amount_ID"] = "Print_Amount.Print_Amount_ID";
            Relation["PrintAmountInfo.Print_Amount_MinArea"] = "Print_Amount.Print_Amount_MinArea";
            Relation["PrintAmountInfo.Print_Amount_MaxArea"] = "Print_Amount.Print_Amount_MaxArea";
            Relation["PrintAmountInfo.Print_Amount_Coefficient"] = "Print_Amount.Print_Amount_Coefficient";
            Relation["PrintAmountInfo.Print_Material_Name"] = "Print_Amount.Print_Material_Name";
            Relation["PrintAmountInfo.Print_Amount_DieCutting"] = "Print_Amount.Print_Amount_DieCutting";
            Relation["PrintConfigInfo.Print_Config_FinishedRemark"] = "Print_Config.Print_Config_FinishedRemark";

            //Print_Material
            Relation["PrintMaterialInfo.Print_Material_ID"] = "Print_Material.Print_Material_ID";
            Relation["PrintMaterialInfo.Print_Material_Name"] = "Print_Material.Print_Material_Name";
            Relation["PrintMaterialInfo.Print_Material_PrintCoefficient"] = "Print_Material.Print_Material_PrintCoefficient";
            Relation["PrintMaterialInfo.Print_Material_Price"] = "Print_Material.Print_Material_Price";
            Relation["PrintMaterialInfo.Print_Material_PrintingCoefficient"] = "Print_Material.Print_Material_PrintingCoefficient";
            Relation["PrintMaterialInfo.Print_Material_img"] = "Print_Material.Print_Material_img";
            Relation["PrintMaterialInfo.Print_Material_Remark"] = "Print_Material.Print_Material_Remark";


            //Member_Invoice
            Relation["MemberInvoiceInfo.Invoice_ID"] = "Member_Invoice.Invoice_ID";
            Relation["MemberInvoiceInfo.Invoice_MemberID"] = "Member_Invoice.Invoice_MemberID";
            Relation["MemberInvoiceInfo.Invoice_VAT_FirmName"] = "Member_Invoice.Invoice_VAT_FirmName";
            Relation["MemberInvoiceInfo.Invoice_VAT_Code"] = "Member_Invoice.Invoice_VAT_Code";
            Relation["MemberInvoiceInfo.Invoice_VAT_RegAddr"] = "Member_Invoice.Invoice_VAT_RegAddr";
            Relation["MemberInvoiceInfo.Invoice_VAT_RegTel"] = "Member_Invoice.Invoice_VAT_RegTel";
            Relation["MemberInvoiceInfo.Invoice_VAT_Bank"] = "Member_Invoice.Invoice_VAT_Bank";
            Relation["MemberInvoiceInfo.Invoice_VAT_BankAcount"] = "Member_Invoice.Invoice_VAT_BankAcount";
            Relation["MemberInvoiceInfo.Invoice_VAT_Content"] = "Member_Invoice.Invoice_VAT_Content";
            Relation["MemberInvoiceInfo.Invoice_VAT_Annex"] = "Member_Invoice.Invoice_VAT_Annex";
            Relation["MemberInvoiceInfo.Invoice_VAT_IsAudit"] = "Member_Invoice.Invoice_VAT_IsAudit";
            Relation["MemberInvoiceInfo.Invoice_VAT_Remarks"] = "Member_Invoice.Invoice_VAT_Remarks";
            Relation["MemberInvoiceInfo.Invoice_AddTime"] = "Member_Invoice.Invoice_AddTime";
            Relation["MemberInvoiceInfo.Invoice_EditTime"] = "Member_Invoice.Invoice_EditTime";
            Relation["MemberInvoiceInfo.Invoice_AuditTime"] = "Member_Invoice.Invoice_AuditTime";


            //Member_ApplyInvoice
            Relation["MemberApplyInvoiceInfo.Invoice_ID"] = "Member_ApplyInvoice.Invoice_ID";
            Relation["MemberApplyInvoiceInfo.Invoice_MemberID"] = "Member_ApplyInvoice.Invoice_MemberID";
            Relation["MemberApplyInvoiceInfo.Invoice_VAT_FirmName"] = "Member_ApplyInvoice.Invoice_VAT_FirmName";
            Relation["MemberApplyInvoiceInfo.Invoice_VAT_Code"] = "Member_ApplyInvoice.Invoice_VAT_Code";
            Relation["MemberApplyInvoiceInfo.Invoice_VAT_RegAddr"] = "Member_ApplyInvoice.Invoice_VAT_RegAddr";
            Relation["MemberApplyInvoiceInfo.Invoice_VAT_RegTel"] = "Member_ApplyInvoice.Invoice_VAT_RegTel";
            Relation["MemberApplyInvoiceInfo.Invoice_VAT_Bank"] = "Member_ApplyInvoice.Invoice_VAT_Bank";
            Relation["MemberApplyInvoiceInfo.Invoice_VAT_BankAcount"] = "Member_ApplyInvoice.Invoice_VAT_BankAcount";
            Relation["MemberApplyInvoiceInfo.Invoice_VAT_Content"] = "Member_ApplyInvoice.Invoice_VAT_Content";
            Relation["MemberApplyInvoiceInfo.Invoice_VAT_Annex"] = "Member_ApplyInvoice.Invoice_VAT_Annex";
            Relation["MemberApplyInvoiceInfo.Invoice_Addtime"] = "Member_ApplyInvoice.Invoice_Addtime";
            Relation["MemberApplyInvoiceInfo.IsAudit"] = "Member_ApplyInvoice.IsAudit";
            Relation["MemberApplyInvoiceInfo.Invoice_Audittime"] = "Member_ApplyInvoice.Invoice_Audittime";
            Relation["MemberApplyInvoiceInfo.AuditName"] = "Member_ApplyInvoice.AuditName";
            Relation["MemberApplyInvoiceInfo.Remarks"] = "Member_ApplyInvoice.Remarks";

            //Orders_BackApply_img
            Relation["OrdersBackApplyimgInfo.ID"] = "Orders_BackApply_img.ID";
            Relation["OrdersBackApplyimgInfo.Orders_BackApplyID"] = "Orders_BackApply_img.Orders_BackApplyID";
            Relation["OrdersBackApplyimgInfo.img"] = "Orders_BackApply_img.img";


            //Consolidate_Order
            Relation["ConsolidateOrderInfo.Consolidate_Order_ID"] = "Consolidate_Order.Consolidate_Order_ID";
            Relation["ConsolidateOrderInfo.Consolidate_Order_SN"] = "Consolidate_Order.Consolidate_Order_SN";
            Relation["ConsolidateOrderInfo.Consolidate_Order_BuyerID"] = "Consolidate_Order.Consolidate_Order_BuyerID";
            Relation["ConsolidateOrderInfo.Consolidate_Order_Status"] = "Consolidate_Order.Consolidate_Order_Status";
            Relation["ConsolidateOrderInfo.Consolidate_Order_PaymentStatus"] = "Consolidate_Order.Consolidate_Order_PaymentStatus";
            Relation["ConsolidateOrderInfo.Consolidate_Order_PaymentStatus_Time"] = "Consolidate_Order.Consolidate_Order_PaymentStatus_Time";
            Relation["ConsolidateOrderInfo.Consolidate_Order_Fail_Note"] = "Consolidate_Order.Consolidate_Order_Fail_Note";
            Relation["ConsolidateOrderInfo.Consolidate_Order_Fail_Addtime"] = "Consolidate_Order.Consolidate_Order_Fail_Addtime";
            Relation["ConsolidateOrderInfo.Consolidate_Order_Total_AllPrice"] = "Consolidate_Order.Consolidate_Order_Total_AllPrice";
            Relation["ConsolidateOrderInfo.Consolidate_Order_Total_Discount"] = "Consolidate_Order.Consolidate_Order_Total_Discount";
            Relation["ConsolidateOrderInfo.Consolidate_Order_Total_RemainPrice"] = "Consolidate_Order.Consolidate_Order_Total_RemainPrice";
            Relation["ConsolidateOrderInfo.Consolidate_Order_Addtime"] = "Consolidate_Order.Consolidate_Order_Addtime";
            Relation["ConsolidateOrderInfo.Consolidate_Order_Payway"] = "Consolidate_Order.Consolidate_Order_Payway";
            Relation["ConsolidateOrderInfo.Consolidate_Order_Payway_Name"] = "Consolidate_Order.Consolidate_Order_Payway_Name";
            Relation["ConsolidateOrderInfo.Consolidate_Orderl_Note"] = "Consolidate_Order.Consolidate_Orderl_Note";

            //Consolidate_Order_view
            Relation["ConsolidateOrderviewInfo.ID"] = "Consolidate_Order_view.ID";
            Relation["ConsolidateOrderviewInfo.Consolidate_Order_ID"] = "Consolidate_Order_view.Consolidate_Order_ID";
            Relation["ConsolidateOrderviewInfo.OrderType"] = "Consolidate_Order_view.OrderType";
            Relation["ConsolidateOrderviewInfo.OrderID"] = "Consolidate_Order_view.OrderID";
            Relation["ConsolidateOrderviewInfo.Price"] = "Consolidate_Order_view.Price";

            //Consolidate_Policy
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_ID"] = "Consolidate_Policy.Consolidate_Policy_ID";
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_Payline"] = "Consolidate_Policy.Consolidate_Policy_Payline";
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_Manner"] = "Consolidate_Policy.Consolidate_Policy_Manner";
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_Price"] = "Consolidate_Policy.Consolidate_Policy_Price";
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_Percent"] = "Consolidate_Policy.Consolidate_Policy_Percent";
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_IsRepeat"] = "Consolidate_Policy.Consolidate_Policy_IsRepeat";
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_Sort"] = "Consolidate_Policy.Consolidate_Policy_Sort";
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_IsActive"] = "Consolidate_Policy.Consolidate_Policy_IsActive";
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_IsChecked"] = "Consolidate_Policy.Consolidate_Policy_IsChecked";
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_Note"] = "Consolidate_Policy.Consolidate_Policy_Note";
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_Site"] = "Consolidate_Policy.Consolidate_Policy_Site";
            Relation["ConsolidatePolicyinfo.Consolidate_Policy_Name"] = "Consolidate_Policy.Consolidate_Policy_Name";

            //Consolidate_Policy_MemberGrade
            Relation["ConsolidatePolicyMemberGradeInfo.ID"] = "Consolidate_Policy_MemberGrade.ID";
            Relation["ConsolidatePolicyMemberGradeInfo.Policy_ID"] = "Consolidate_Policy_MemberGrade.Policy_ID";
            Relation["ConsolidatePolicyMemberGradeInfo.MemberGradeID"] = "Consolidate_Policy_MemberGrade.MemberGradeID";

            //Consolidate_Order_log
            Relation["ConsolidateOrderlogInfo.Log_ID"] = "Consolidate_Order_log.Log_ID";
            Relation["ConsolidateOrderlogInfo.Log_OrdersID"] = "Consolidate_Order_log.Log_OrdersID";
            Relation["ConsolidateOrderlogInfo.Log_Addtime"] = "Consolidate_Order_log.Log_Addtime";
            Relation["ConsolidateOrderlogInfo.Log_Operator"] = "Consolidate_Order_log.Log_Operator";
            Relation["ConsolidateOrderlogInfo.Log_Remark"] = "Consolidate_Order_log.Log_Remark";
            Relation["ConsolidateOrderlogInfo.Log_Action"] = "Consolidate_Order_log.Log_Action";
            Relation["ConsolidateOrderlogInfo.Log_Result"] = "Consolidate_Order_log.Log_Result";

            //Consolidate_Order_Payment
            Relation["ConsolidateOrderPaymentInfo.Orders_Payment_ID"] = "Consolidate_Order_Payment.Orders_Payment_ID";
            Relation["ConsolidateOrderPaymentInfo.Orders_Payment_OrdersID"] = "Consolidate_Order_Payment.Orders_Payment_OrdersID";
            Relation["ConsolidateOrderPaymentInfo.Orders_Payment_PaymentStatus"] = "Consolidate_Order_Payment.Orders_Payment_PaymentStatus";
            Relation["ConsolidateOrderPaymentInfo.Orders_Payment_SysUserID"] = "Consolidate_Order_Payment.Orders_Payment_SysUserID";
            Relation["ConsolidateOrderPaymentInfo.Orders_Payment_DocNo"] = "Consolidate_Order_Payment.Orders_Payment_DocNo";
            Relation["ConsolidateOrderPaymentInfo.Orders_Payment_Name"] = "Consolidate_Order_Payment.Orders_Payment_Name";
            Relation["ConsolidateOrderPaymentInfo.Orders_Payment_Amount"] = "Consolidate_Order_Payment.Orders_Payment_Amount";
            Relation["ConsolidateOrderPaymentInfo.Orders_Payment_Note"] = "Consolidate_Order_Payment.Orders_Payment_Note";
            Relation["ConsolidateOrderPaymentInfo.Orders_Payment_Addtime"] = "Consolidate_Order_Payment.Orders_Payment_Addtime";
            Relation["ConsolidateOrderPaymentInfo.Orders_Payment_Site"] = "Consolidate_Order_Payment.Orders_Payment_Site";

            //Invoice
            Relation["InvoiceInfo.Invoice_ID"] = "Invoice.Invoice_ID";
            Relation["InvoiceInfo.Invoice_MemberID"] = "Invoice.Invoice_MemberID";
            Relation["InvoiceInfo.Invoice_VAT_FirmName"] = "Invoice.Invoice_VAT_FirmName";
            Relation["InvoiceInfo.Invoice_VAT_Code"] = "Invoice.Invoice_VAT_Code";
            Relation["InvoiceInfo.Invoice_VAT_RegAddr"] = "Invoice.Invoice_VAT_RegAddr";
            Relation["InvoiceInfo.Invoice_VAT_RegTel"] = "Invoice.Invoice_VAT_RegTel";
            Relation["InvoiceInfo.Invoice_VAT_Bank"] = "Invoice.Invoice_VAT_Bank";
            Relation["InvoiceInfo.Invoice_VAT_BankAcount"] = "Invoice.Invoice_VAT_BankAcount";
            Relation["InvoiceInfo.Invoice_VAT_Content"] = "Invoice.Invoice_VAT_Content";
            Relation["InvoiceInfo.Invoice_VAT_Annex"] = "Invoice.Invoice_VAT_Annex";
            Relation["InvoiceInfo.Invoice_Status"] = "Invoice.Invoice_Status";
            Relation["InvoiceInfo.Invoice_Price"] = "Invoice.Invoice_Price";
            Relation["InvoiceInfo.Invoice_SN"] = "Invoice.Invoice_SN";
            Relation["InvoiceInfo.Invoice_AddTime"] = "Invoice.Invoice_AddTime";

            //Invoice_view
            Relation["InvoiceviewInfo.ID"] = "Invoice_view.ID";
            Relation["InvoiceviewInfo.Invoice_ID"] = "Invoice_view.Invoice_ID";
            Relation["InvoiceviewInfo.Order_Type"] = "Invoice_view.Order_Type";
            Relation["InvoiceviewInfo.Order_ID"] = "Invoice_view.Order_ID";
            Relation["InvoiceviewInfo.Order_SN"] = "Invoice_view.Order_SN";
            Relation["InvoiceviewInfo.Order_Price"] = "Invoice_view.Order_Price";





            //Member_ApplyAuthorization
            Relation["MemberApplyAuthorizationInfo.ID"] = "Member_ApplyAuthorization.ID";
            Relation["MemberApplyAuthorizationInfo.Member_ID"] = "Member_ApplyAuthorization.Member_ID";
            Relation["MemberApplyAuthorizationInfo.Number"] = "Member_ApplyAuthorization.Number";
            Relation["MemberApplyAuthorizationInfo.Addtime"] = "Member_ApplyAuthorization.Addtime";
            Relation["MemberApplyAuthorizationInfo.Remark"] = "Member_ApplyAuthorization.Remark";
            Relation["MemberApplyAuthorizationInfo.IsAudit"] = "Member_ApplyAuthorization.IsAudit";
            Relation["MemberApplyAuthorizationInfo.AuditRemark"] = "Member_ApplyAuthorization.AuditRemark";
            Relation["MemberApplyAuthorizationInfo.AuditAddtime"] = "Member_ApplyAuthorization.AuditAddtime";
            Relation["MemberApplyAuthorizationInfo.Operator"] = "Member_ApplyAuthorization.Operator";


            //Member_ApplyMember
            Relation["MemberApplyMemberInfo.ID"] = "Member_ApplyMember.ID";
            Relation["MemberApplyMemberInfo.Partner"] = "Member_ApplyMember.Partner";
            Relation["MemberApplyMemberInfo.MemberID"] = "Member_ApplyMember.MemberID";
            Relation["MemberApplyMemberInfo.NickName"] = "Member_ApplyMember.NickName";
            Relation["MemberApplyMemberInfo.Addtime"] = "Member_ApplyMember.Addtime";
            Relation["MemberApplyMemberInfo.IsAudit"] = "Member_ApplyMember.IsAudit";
            Relation["MemberApplyMemberInfo.AuditRemark"] = "Member_ApplyMember.AuditRemark";
            Relation["MemberApplyMemberInfo.AuditAddtime"] = "Member_ApplyMember.AuditAddtime";
            Relation["MemberApplyMemberInfo.Operator"] = "Member_ApplyMember.Operator";

            //Member_Favor_Policy
            Relation["MemberFavorPolicyInfo.ID"] = "Member_Favor_Policy.ID";
            Relation["MemberFavorPolicyInfo.PartnerID"] = "Member_Favor_Policy.PartnerID";
            Relation["MemberFavorPolicyInfo.Partner"] = "Member_Favor_Policy.Partner";
            Relation["MemberFavorPolicyInfo.MemberID"] = "Member_Favor_Policy.MemberID";
            Relation["MemberFavorPolicyInfo.MemberName"] = "Member_Favor_Policy.MemberName";
            Relation["MemberFavorPolicyInfo.Addtime"] = "Member_Favor_Policy.Addtime";
            Relation["MemberFavorPolicyInfo.Apply_Type"] = "Member_Favor_Policy.Apply_Type";
            Relation["MemberFavorPolicyInfo.Category_ID"] = "Member_Favor_Policy.Category_ID";
            Relation["MemberFavorPolicyInfo.Member_Percent"] = "Member_Favor_Policy.Member_Percent";
            Relation["MemberFavorPolicyInfo.IsAudit"] = "Member_Favor_Policy.IsAudit";
            Relation["MemberFavorPolicyInfo.AuditAddtime"] = "Member_Favor_Policy.AuditAddtime";
            Relation["MemberFavorPolicyInfo.Remarks"] = "Member_Favor_Policy.Remarks";
            Relation["MemberFavorPolicyInfo.Operator"] = "Member_Favor_Policy.Operator";

            //Keyword
            Relation["KeywordInfo.Keyword_ID"] = "Keyword.Keyword_ID";
            Relation["KeywordInfo.Keyword_Name"] = "Keyword.Keyword_Name";
            Relation["KeywordInfo.Keyword_Type"] = "Keyword.Keyword_Type";
            Relation["KeywordInfo.Keyword_Time"] = "Keyword.Keyword_Time";
            Relation["KeywordInfo.Keyword_IP"] = "Keyword.Keyword_IP";

            //Download_Log
            Relation["DownloadLogInfo.Download_ID"] = "Download_Log.Download_ID";
            Relation["DownloadLogInfo.Download_Name"] = "Download_Log.Download_Name";
            Relation["DownloadLogInfo.Download_Type"] = "Download_Log.Download_Type";
            Relation["DownloadLogInfo.Download_IP"] = "Download_Log.Download_IP";
            Relation["DownloadLogInfo.Download_Addr"] = "Download_Log.Download_Addr";
            Relation["DownloadLogInfo.Download_Count"] = "Download_Log.Download_Count";
            Relation["DownloadLogInfo.Download_AddTime"] = "Download_Log.Download_AddTime";

            //Big_Customer
            Relation["BigCustomerInfo.Big_Customer_ID"] = "Big_Customer.Big_Customer_ID";
            Relation["BigCustomerInfo.Big_Customer_Type"] = "Big_Customer.Big_Customer_Type";
            Relation["BigCustomerInfo.Big_Customer_Logo"] = "Big_Customer.Big_Customer_Logo";
            Relation["BigCustomerInfo.Big_Customer_Banner"] = "Big_Customer.Big_Customer_Banner";
            Relation["BigCustomerInfo.Big_Customer_Addtime"] = "Big_Customer.Big_Customer_Addtime";
            Relation["BigCustomerInfo.Big_Customer_Name"] = "Big_Customer.Big_Customer_Name";
            Relation["BigCustomerInfo.Big_Customer_CompanyName"] = "Big_Customer.Big_Customer_CompanyName";
            Relation["BigCustomerInfo.Big_Customer_Password"] = "Big_Customer.Big_Customer_Password";

            //Big_Customer_RelateMember
            Relation["BigCustomerRelateMemberInfo.ID"] = "Big_Customer_RelateMember.ID";
            Relation["BigCustomerRelateMemberInfo.BigCustomer_ID"] = "Big_Customer_RelateMember.BigCustomer_ID";
            Relation["BigCustomerRelateMemberInfo.Member_ID"] = "Big_Customer_RelateMember.Member_ID";

            //Big_Customer_RelateProduct
            Relation["BigCustomerRelateProductInfo.ID"] = "Big_Customer_RelateProduct.ID";
            Relation["BigCustomerRelateProductInfo.BigCustomer_ID"] = "Big_Customer_RelateProduct.BigCustomer_ID";
            Relation["BigCustomerRelateProductInfo.Product_ID"] = "Big_Customer_RelateProduct.Product_ID";

            //RBAC_User_RelateCustomer
            Relation["RBACUserRelateCustomerInfo.ID"] = "RBAC_User_RelateCustomer.ID";
            Relation["RBACUserRelateCustomerInfo.UserID"] = "RBAC_User_RelateCustomer.UserID";
            Relation["RBACUserRelateCustomerInfo.CustomerID"] = "RBAC_User_RelateCustomer.CustomerID";


            //SensitiveWords
            Relation["SensitiveWordsInfo.ID"] = "SensitiveWords.ID";
            Relation["SensitiveWordsInfo.Name"] = "SensitiveWords.Name";

            //Special
            //Special
            Relation["SpecialInfo.Special_ID"] = "Special.Special_ID";
            Relation["SpecialInfo.Special_Title"] = "Special.Special_Title";
            Relation["SpecialInfo.Special_Intro"] = "Special.Special_Intro";
            Relation["SpecialInfo.Special_Img"] = "Special.Special_Img";
            Relation["SpecialInfo.Special_BannerImg"] = "Special.Special_BannerImg";
            Relation["SpecialInfo.Special_Sort"] = "Special.Special_Sort";
            Relation["SpecialInfo.Special_IsRecommend"] = "Special.Special_IsRecommend";
            Relation["SpecialInfo.Special_IsAudit"] = "Special.Special_IsAudit";
            Relation["SpecialInfo.Special_Site"] = "Special.Special_Site";
            Relation["SpecialInfo.Special_Addtime"] = "Special.Special_Addtime";
            Relation["SpecialInfo.Special_CateID"] = "Special.Special_CateID";

            //Article_Category
            Relation["ArticleCategoryInfo.Article_Category_ID"] = "Article_Category.Article_Category_ID";
            Relation["ArticleCategoryInfo.Article_Category_ArticleID"] = "Article_Category.Article_Category_ArticleID";
            Relation["ArticleCategoryInfo.Article_Category_CategoryID"] = "Article_Category.Article_Category_CategoryID";



            //Article_Subject
            Relation["ArticleSubjectInfo.Subject_ID"] = "Article_Subject.Subject_ID";
            Relation["ArticleSubjectInfo.Subject_Name"] = "Article_Subject.Subject_Name";
            Relation["ArticleSubjectInfo.Subject_Img"] = "Article_Subject.Subject_Img";
            Relation["ArticleSubjectInfo.Subject_IsActive"] = "Article_Subject.Subject_IsActive";
            Relation["ArticleSubjectInfo.Subject_Sort"] = "Article_Subject.Subject_Sort";
            Relation["ArticleSubjectInfo.Subject_Site"] = "Article_Subject.Subject_Site";

            //Question_Cate
            Relation["QuestionCateInfo.ID"] = "Question_Cate.ID";
            Relation["QuestionCateInfo.Q_Cate_Name"] = "Question_Cate.Q_Cate_Name";
            Relation["QuestionCateInfo.Q_Cate_Valid"] = "Question_Cate.Q_Cate_Valid";

            //Question
            Relation["QuestionInfo.ID"] = "Question.ID";
            Relation["QuestionInfo.Q_Cate"] = "Question.Q_Cate";
            Relation["QuestionInfo.Q_Question"] = "Question.Q_Question";
            Relation["QuestionInfo.Q_Option_A"] = "Question.Q_Option_A";
            Relation["QuestionInfo.Q_Option_B"] = "Question.Q_Option_B";
            Relation["QuestionInfo.Q_Option_C"] = "Question.Q_Option_C";
            Relation["QuestionInfo.Q_Option_D"] = "Question.Q_Option_D";
            Relation["QuestionInfo.Q_Answer"] = "Question.Q_Answer";

            //Question_History
            Relation["QuestionHistoryInfo.ID"] = "Question_History.ID";
            Relation["QuestionHistoryInfo.Q"] = "Question_History.Q";
            Relation["QuestionHistoryInfo.Q_Hit"] = "Question_History.Q_Hit";
            Relation["QuestionHistoryInfo.Q_AddDate"] = "Question_History.Q_AddDate";

            //Vote
            Relation["VoteInfo.Vote_ID"] = "Vote.Vote_ID";
            Relation["VoteInfo.Vote_Name"] = "Vote.Vote_Name";
            Relation["VoteInfo.Vote_Source"] = "Vote.Vote_Source";
            Relation["VoteInfo.Vote_Start"] = "Vote.Vote_Start";
            Relation["VoteInfo.Vote_End"] = "Vote.Vote_End";
            Relation["VoteInfo.Vote_IsActive"] = "Vote.Vote_IsActive";
            Relation["VoteInfo.Vote_Number"] = "Vote.Vote_Number";
            Relation["VoteInfo.Vote_AddTime"] = "Vote.Vote_AddTime";
            Relation["VoteInfo.Vote_Remarks"] = "Vote.Vote_Remarks";
            Relation["VoteInfo.Vote_SN"] = "Vote.Vote_SN";
            Relation["VoteInfo.Vote_Type"] = "Vote.Vote_Type";




            //Vote_Select
            Relation["VoteSelectInfo.Vote_Select_ID"] = "Vote_Select.Vote_Select_ID";
            Relation["VoteSelectInfo.Vote_Select_Name"] = "Vote_Select.Vote_Select_Name";
            Relation["VoteSelectInfo.Vote_Select_VoteID"] = "Vote_Select.Vote_Select_VoteID";
            Relation["VoteSelectInfo.Vote_Select_Number"] = "Vote_Select.Vote_Select_Number";

            //Vote_Member
            Relation["VoteMemberInfo.Vote_Member_ID"] = "Vote_Member.Vote_Member_ID";
            Relation["VoteMemberInfo.Vote_Member_VoteID"] = "Vote_Member.Vote_Member_VoteID";
            Relation["VoteMemberInfo.Vote_Member_VoteSelectID"] = "Vote_Member.Vote_Member_VoteSelectID";
            Relation["VoteMemberInfo.Vote_Member_MemberID"] = "Vote_Member.Vote_Member_MemberID";
            Relation["VoteMemberInfo.Vote_Member_AddTime"] = "Vote_Member.Vote_Member_AddTime";
        }

    }
}