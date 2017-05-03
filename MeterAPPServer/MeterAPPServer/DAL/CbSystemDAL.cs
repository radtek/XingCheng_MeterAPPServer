﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAndroid.Models;
using TestAndroid.Models.Request;
using TestAndroid.Models.Entity;
using TestAndroid.Models.Response;
using System.Text;
using MeterAPPServer.Models.Response;
using MeterAPPServer.Models.Request;
using MeterAPPServer.Models.Entity;


namespace TestAndroid.DAL
{
    public class CbSystemDAL : BaseDAL
    {
        // private string chargeID = string.Empty;

        /// <summary>
        ///  RONG
        ///  2016-5-2
        ///  判断是否是在规定时间内上传
        /// DECLARE @LoginID varchar(50)='0018'
        /// DECLARE @Today INT=20
        ///SELECT * FROM base_login WHERE userstate=1 AND LOGINID=@LoginID AND @Today BETWEEN MeterDateTimeBegin AND MeterDateTimeEnd
        /// </summary>
        /// <param name="loginid"></param>
        /// <returns></returns>
        #region
        private bool IsUploadExtent(string loginid)
        {
            bool result = false;

            using (var content = WDbContext())
            {
                //declare @LoginID int =0026
                //SELECT * FROM base_login WHERE userstate=1 AND LOGINID=@LoginID AND @Today > CONVERT(datetime,CONVERT(char(8),@Today,120)+CONVERT(char(8),MeterDateTimeBegin)) AND @Today<Datename(year,GetDate())+'-'+Datename(month,GetDate())+'-'+Datename(day,MeterDateTimeEnd-1)+' 08:00:00'
                string strsql = "SELECT Count(1) FROM base_login WHERE userstate=1 AND LOGINID=@LoginID AND getdate() > CONVERT(datetime,CONVERT(char(8),getdate(),120)+CONVERT(char(8),MeterDateTimeBegin)) AND getdate()<Datename(year,GetDate())+'-'+Datename(month,GetDate())+'-'+Datename(day,MeterDateTimeEnd-1)+' 08:00:00'";
                var retItems = content.Sql(strsql)
                    .Parameter("LoginID", loginid)
                    .QuerySingle<int>();
                if (retItems != 0)
                {
                    result = true;
                }
            }

            return result;
        }

        #endregion

        public EquipmentRes Reg(EquipmentReg Ereg)
        {
            var retItem = new EquipmentRes();
            using (var context = WDbContext())
            {
                //首先查询数据库中是否存在
                string strSql = "select MEID,MECode,LoginID,States from MeterEquipment where MECode=@MECode";
                var retItems = context.Sql(strSql)
                    .Parameter("MECode", Ereg.MECode)
                    .QueryMany<EquipmentItem>();
                if (retItems == null || retItems.Count == 0)
                {
                    //插入CODE记录
                    strSql = " INSERT INTO MeterEquipment (MECode) VALUES (@MECode)";
                    context.Sql(strSql)
                       .Parameter("MECode", Ereg.MECode).Execute();
                    retItem = new EquipmentRes();
                    retItem.isErrMsg = false;
                    retItem.errMsg = "";
                    return retItem;
                }
                else
                {
                    if (retItems.Count == 1)
                    {
                        if (retItems[0].LoginID == null || retItems[0].States != 0)
                        {
                            retItem.isErrMsg = true;
                            retItem.errMsg = "该设备暂不可用";
                            retItem.errMsgNo = 1;
                            return retItem;
                        }
                    }
                    else
                    {
                        retItems = retItems.Where(c => c.States == 0 && c.LoginID != null).ToList();
                    }
                }
                if (retItems == null || retItems.Count == 0)
                {
                    retItem.isErrMsg = true;
                    retItem.errMsg = "没有查询到数据";
                    retItem.errMsgNo = 1;
                }
                retItem.equipmentItems = retItems;
                return retItem;
            }
        }

        public LoginRes Login(LoginReq req)
        {
            using (var context = WDbContext())
            {
                string strSql = "SELECT LOGINID,USERNAME,departmentId,departmentName,isnull(telePhoneNo,'')as telePhoneNo,MeterDateTimeBegin,MeterDateTimeEnd,IsPrinter FROM V_LOGIN  where userstate=1 AND LOGINNAME=@account and LOGINPASSWORD=@pwd";
                var retItem = context.Sql(strSql)
                    .Parameter("account", req.LOGINNAME)
                    .Parameter("pwd", req.LOGINPASSWORD)
                    .QuerySingle<LoginRes>();
                if (retItem == null)
                {
                    retItem = new LoginRes();
                    retItem.errMsg = "用户名或密码错误！";
                    retItem.isErrMsg = true;
                    retItem.errMsgNo = 1;
                }
                return retItem;
            }
        }

        //        public WBBRes GetBBInfo(WBBReq bbreq)
        //        {
        //            using (var context = WDbContext())
        //            {
        //                string strSql = @"DECLARE @maxYear INT=0
        //                                DECLARE @maxMonth INT=0
        //
        //                                SELECT @maxYear=MAX(readMeterRecordYear) FROM readMeterRecord WHERE loginId=@userid
        //                                SELECT @maxMonth=MAX(readMeterRecordMonth) FROM readMeterRecord 
        //                                WHERE readMeterRecordYear=@maxYear AND loginId=@userid
        //
        //                                SELECT meterReadingNO as NoteNo,count(1)AS CustomerCount,readMeterRecordYear as CBYear,readMeterRecordMonth as CBMonth FROM readMeterRecord
        //                                WHERE readMeterRecordYear=@maxYear AND readMeterRecordMonth=@maxMonth AND loginId=@userid
        //                                GROUP BY meterReadingNO,readMeterRecordYear,readMeterRecordMonth order by meterReadingNO";
        //                var retItems = context.Sql(strSql)
        //                    .Parameter("userid", bbreq.LoginID)
        //                    .QueryMany<WBBItem>();
        //                WBBRes res = new WBBRes();
        //                res.bbItems = retItems;
        //                return res;
        //            }
        //        }
        public WBBRes GetBBInfo(WBBReq bbreq)
        {
            using (var context = WDbContext())
            {
                string strSql = @"Declare @MaxDate datetime
                                declare @maxStrDate nvarchar(7)
                                SELECT @MaxDate=MAX(readMeterRecordYearAndMonth) FROM readMeterRecord WHERE loginId=@userid

                                select @maxStrDate=CONVERT(varchar(7),@MaxDate,120)

                                SELECT tt1.NoteNo,tt1.CustomerCount,DATEPART(YEAR,@MaxDate)as CBYear,DATEPART(Month,@MaxDate)as CBMonth,tt2.meterReadingNO AS AreaNo FROM
                                (
                                SELECT meterReadingID as NoteNo,count(1)AS CustomerCount FROM readMeterRecord WITH(nolock)
                                WHERE CONVERT(varchar(7),readMeterRecordYearAndMonth,120)=@maxStrDate AND loginId=@userid
                                GROUP BY meterReadingID 
                                )tt1
                                JOIN
                                (
                                SELECT b.areaId,b.areaName,r.meterReadingID,r.meterReadingNO FROM base_area b WITH(nolock)
                                JOIN meterReading r WITH(nolock) on b.areaId=r.AREAID
                                )tt2
                                ON tt1.NoteNo=tt2.meterReadingID
                                ORDER BY tt1.NoteNo
                                ";
                var retItems = context.Sql(strSql)
                    .Parameter("userid", bbreq.LoginID)
                    .QueryMany<WBBItem>();
                WBBRes res = new WBBRes();
                res.bbItems = retItems;
                return res;
            }
        }

        public WUserItemRes GetUserItemRes(WUserItemsReq req)
        {
            using (var context = WDbContext())
            {

                string strSql = @"SELECT 
                                readMeterRecordId,
                                meterReadingId as NoteNo,
                                waterMeterNo as StealNo,
                                waterUserAddress as Address,
                                lastNumberYearMonth as LastChaoBiaoDate,
                                waterMeterLastNumber as LastMonthValue,
                                waterMeterEndNumber as CurrentMonthValue,
                                totalNumber as CurrMonthWNum,
                                avePrice,
                                waterTotalCharge as CurrMonthFee,
                                extraChargePrice1,
                                extraCharge1,
                                extraChargePrice2,
                                extraCharge2,
                                extraTotalCharge,
                                trapezoidPrice as  StepPrice,
                                extraCharge as ExtraPrice,
                                totalCharge as TotalCharge,
                                OVERDUEMONEY as OVERDUEMONEY,
                                WATERFIXVALUE as WATERFIXVALUE,
                                datepart(year,readMeterRecordYearAndMonth) ReadMeterRecordYear,
                                datepart(month,readMeterRecordYearAndMonth) ReadMeterRecordMonth,
                                readMeterRecordDate as ChaoBiaoDate,
                                WaterMeterPositionName,
                                loginId,
                                USERNAME,
                                checkState,
                                checkDateTime,
                                checker,
                                chargeState as ChaoBiaoTag,
                                chargeID,
                                waterUserId,
                                waterUserNO as UserNo,
                                waterUserName as UserFName,
                                waterPhone as Phone,
                                IsSummaryMeter,
                                WaterMeterParentID,
                                Latitude,
                                Longitude,
                                OrderNumber,
                                waterUserTypeId as PriceType,
                                waterusertypename as PriceTypeName,
                                WATERMETERNUMBERCHANGESTATE,
                                waterUserchargeType,
                                Memo1,
                                totalNumberFirst
                                ,avePriceFirst
                                ,waterTotalChargeFirst
                                ,totalNumberSencond
                                ,avePriceSencond
                                ,waterTotalChargeSencond
                                ,totalNumberThird
                                ,avePriceThird
                                ,waterTotalChargeThird
                                 FROM readMeterRecord WHERE loginId=@loginid
                                AND 
                                WATERMETERNUMBERCHANGESTATE=0
                                and meterReadingId=@meterReadingNO
                                and convert(varchar(7),readMeterRecordYearAndMonth,120)=@currMonth
                                AND totalCharge>=0
                                ORDER BY ordernumber ASC";

                var userItems = context.Sql(strSql)
                                   .Parameter("loginid", req.loginid)
                                   .Parameter("cbYear", req.cbYear)
                                   .Parameter("currMonth",req.cbYear+"-"+req.cbMonth.ToString().PadLeft(2,'0'))
                                   .Parameter("meterReadingNO", req.meterReadingNO)
                                   .QueryMany<WUserItem>();

                WUserItemRes res = new WUserItemRes();
                res.userItems = userItems;
                return res;

            }
        }
        //获取单条记录
        public WUserItemRes GetSingleUserItemRes(WSingleUserItemReq req)
        {
            using (var context = WDbContext())
            {

                string strSql = @"SELECT 
                                readMeterRecordId,
                                meterReadingID as NoteNo,
                                waterMeterNo as StealNo,
                                waterUserAddress as Address,
                                lastNumberYearMonth as LastChaoBiaoDate,
                                waterMeterLastNumber as LastMonthValue,
                                waterMeterEndNumber as CurrentMonthValue,
                                totalNumber as CurrMonthWNum,
                                avePrice,
                                waterTotalCharge as CurrMonthFee,
                                extraChargePrice1,
                                extraCharge1,
                                extraChargePrice2,
                                extraCharge2,
                                extraTotalCharge,
                                trapezoidPrice as  StepPrice,
                                extraCharge as ExtraPrice,
                                totalCharge as TotalCharge,
                                OVERDUEMONEY as OVERDUEMONEY,
                                WATERFIXVALUE as WATERFIXVALUE,
                                datepart(year,readMeterRecordYearAndMonth) ReadMeterRecordYear,
                                datepart(month,readMeterRecordYearAndMonth) ReadMeterRecordMonth,
                                readMeterRecordDate as ChaoBiaoDate,
                                WaterMeterPositionName,
                                loginId,
                                USERNAME,
                                checkState,
                                checkDateTime,
                                checker,
                                chargeState as ChaoBiaoTag,
                                chargeID,
                                waterUserId,
                                waterUserNO as UserNo,
                                waterUserName as UserFName,
                                waterPhone as Phone,
                                IsSummaryMeter,
                                WaterMeterParentID,
                                Latitude,
                                Longitude,
                                OrderNumber,
                                waterUserTypeId as PriceType,
                                waterusertypename as PriceTypeName,
                                WATERMETERNUMBERCHANGESTATE,
                                waterUserchargeType,
                                Memo1
                                 FROM readMeterRecord WHERE readMeterRecordId=@readMeterRecordId";

                var userItems = context.Sql(strSql)
                                   .Parameter("readMeterRecordId", req.readMeterRecordId)
                                   .QueryMany<WUserItem>();

                WUserItemRes res = new WUserItemRes();
                res.userItems = userItems;
                return res;

            }
        }
        public WCBHistoryRes GetUserHistory(WCBHistoryReq req)
        {
            var retItem = new WCBHistoryRes();
            if (req == null)
            {
                retItem.errMsg = "参数传递错误";
                retItem.isErrMsg = true;
                return retItem;
            }
            using (var context = WDbContext())
            {
                string strSql = @"SELECT top 1
                                readMeterRecordId,
                                meterReadingID as NoteNo,
                                waterMeterNo as StealNo,
                                waterUserAddress as Address,
                                lastNumberYearMonth as LastChaoBiaoDate,
                                waterMeterLastNumber as LastMonthValue,
                                waterMeterEndNumber as CurrentMonthValue,
                                totalNumber as CurrMonthWNum,
                                avePrice,
                                waterTotalCharge as CurrMonthFee,
                                extraChargePrice1,
                                extraCharge1,
                                extraChargePrice2,
                                extraCharge2,
                                extraTotalCharge,
                                trapezoidPrice as  StepPrice,
                                extraCharge as ExtraPrice,
                                totalCharge as TotalCharge,
                                OVERDUEMONEY as OVERDUEMONEY,
                                WATERFIXVALUE as WATERFIXVALUE,
                                datepart(year,readMeterRecordYearAndMonth) ReadMeterRecordYear,
                                datepart(month,readMeterRecordYearAndMonth) ReadMeterRecordMonth,
                                readMeterRecordDate as ChaoBiaoDate,
                                WaterMeterPositionName,
                                loginId,
                                USERNAME,
                                checkState,
                                checkDateTime,
                                checker,
                                chargeState as ChaoBiaoTag,
                                chargeID,
                                waterUserId,
                                waterUserNO as UserNo,
                                waterUserName as UserFName,
                                waterPhone as Phone,
                                IsSummaryMeter,
                                WaterMeterParentID,
                                Latitude,
                                Longitude,
                                OrderNumber,
                                waterUserTypeId as PriceType,
                                waterusertypename as PriceTypeName,
                                WATERMETERNUMBERCHANGESTATE,
                                Memo1
                                 FROM readMeterRecord WHERE 
                                WATERMETERNUMBERCHANGESTATE=0
                                and convert(varchar(7),readMeterRecordYearAndMonth,120)=@yearmonth
                                and waterMeterNo=@stealNo
                                ORDER BY ordernumber ASC";

                var userItem = context.Sql(strSql)
                                   .Parameter("yearmonth", req.CbYear+"-"+req.CbMonth.ToString().PadLeft(2,'0'))
                                   .Parameter("stealNo", req.StealNo)
                                   .QuerySingle<WUserItem>();
                retItem.UserItem = userItem;
            }
            return retItem;
        }

        private WUserItem GetMeterDataByReacordID(string readMeterRecordId)
        {
            using (var context = WDbContext())
            {
                string strSql = @"SELECT 
                                readMeterRecordId,
                                meterReadingID as NoteNo,
                                waterMeterNo as StealNo,
                                waterUserAddress as Address,
                                lastNumberYearMonth as LastChaoBiaoDate,
                                waterMeterLastNumber as LastMonthValue,
                                waterMeterEndNumber as CurrentMonthValue,
                                totalNumber as CurrMonthWNum,
                                avePrice,
                                waterTotalCharge as CurrMonthFee,
                                extraChargePrice1,
                                extraCharge1,
                                extraChargePrice2,
                                extraCharge2,
                                extraTotalCharge,
                                trapezoidPrice as  StepPrice,
                                extraCharge as ExtraPrice,
                                totalCharge as TotalCharge,
                                OVERDUEMONEY as OVERDUEMONEY,
                                WATERFIXVALUE as WATERFIXVALUE,
                                datepart(year,readMeterRecordYearAndMonth) ReadMeterRecordYear,
                                datepart(month,readMeterRecordYearAndMonth) ReadMeterRecordMonth,
                                readMeterRecordDate as ChaoBiaoDate,
                                WaterMeterPositionName,
                                loginId,
                                USERNAME,
                                checkState,
                                checkDateTime,
                                checker,
                                chargeState as ChaoBiaoTag,
                                chargeID,
                                waterUserId,
                                waterUserNO as UserNo,
                                waterUserName as UserFName,
                                waterPhone as Phone,
                                IsSummaryMeter,
                                WaterMeterParentID,
                                Latitude,
                                Longitude,
                                OrderNumber,
                                waterUserTypeId as PriceType,
                                waterusertypename as PriceTypeName,
                                WATERMETERNUMBERCHANGESTATE,
                                waterUserchargeType
                                 FROM readMeterRecord WHERE readMeterRecordId=@readMeterRecordId";
                var retItem = context.Sql(strSql)
                    .Parameter("readMeterRecordId", readMeterRecordId)
                    .QuerySingle<WUserItem>();

                return retItem;
            }
        }
        //上传用户数据
        public WUploadUserRes UpdataMeterData(WUploadUserReq req)
        {
            #region
            //IsChaoBiao
            bool IsChaoBiao = false;
            //OrMemoTag
            bool OrMemoTag = false;
            //OrPhoneTag
            bool OrPhoneTag = false;
            //OrGpsTag
            bool OrGpsTag = false;
            //是否收费
            bool IsSF = false;

            var retItem = new WUploadUserRes();
            retItem.errMsg = "";
            retItem.isErrMsg = false;

            var readItem = GetMeterDataByReacordID(req.readMeterRecordId);
            string waterUserchargeType = readItem.waterUserchargeType;

            string ClientChargeState = req.chargeState;
            if (!req.waterMeterEndNumber.Equals(readItem.CurrentMonthValue))
            {
                if (req.chargeState.Equals("1") && readItem.ChaoBiaoTag.Equals("0"))
                {
                    IsChaoBiao = true;
                }
            }
            if (req.Phone!=null)
            {
                if (!req.Phone.Equals(readItem.Phone))
                {
                    OrPhoneTag = true;
                }
            }
            if (req.Memo1!=null)
            {
                if (!req.Memo1.Equals(readItem.Memo1))
                {
                    OrMemoTag = true;
                }
            }
            if (req.Longitude!=null)
            {
                if (!req.Longitude.Equals("0.0"))
                {
                    OrGpsTag = true;
                }
            }
            
            //bool editPhone = false;
            // bool editGps = false;
            #endregion

            #region 是否允许上传
            int CurrentMonth = int.Parse(DateTime.Now.Month.ToString());
            bool IsAllowUpdata = true;
            if (!IsUploadExtent(readItem.loginId))
            {
                IsAllowUpdata = false;
            }
            if (CurrentMonth != readItem.ReadMeterRecordMonth)
            {
                IsAllowUpdata = false;
            }
            #endregion

            //是否有修改数据
            // bool IsEdit = false;
            using (var context = WDbContext())
            {
                StringBuilder sbsql = new StringBuilder();
                sbsql.Append("UPDATE readMeterRecord ");
                sbsql.Append("SET ");

                #region//判断是否在上传月份内
                if (IsAllowUpdata && !ClientChargeState.Equals("0"))
                {
                    //IsEdit = true;
                    if (readItem.ChaoBiaoTag.Equals("0") && (req.avePrice != 0))
                    {
                        sbsql.AppendFormat("waterMeterEndNumber={0},", req.waterMeterEndNumber);
                        sbsql.AppendFormat("totalNumber={0},", req.totalNumber);
                        sbsql.AppendFormat("avePrice={0},", req.avePrice);
                        sbsql.AppendFormat("waterTotalCharge={0},", req.waterTotalCharge);
                        sbsql.AppendFormat("extraChargePrice1={0},", req.extraChargePrice1);
                        sbsql.AppendFormat("extraCharge1={0},", req.extraCharge1);
                        sbsql.AppendFormat("extraChargePrice2={0},", req.extraChargePrice2);
                        sbsql.AppendFormat("extraCharge2={0},", req.extraCharge2);
                        sbsql.AppendFormat("extraTotalCharge={0},", req.extraTotalCharge);
                        sbsql.AppendFormat("totalCharge={0},", req.totalCharge);
                        sbsql.AppendFormat("OVERDUEMONEY={0},", req.OVERDUEMONEY);
                        sbsql.AppendFormat("readMeterRecordDate='{0}',", string.IsNullOrEmpty(req.readMeterRecordDate) ? DateTime.Now.ToString() : req.readMeterRecordDate);
                        sbsql.AppendFormat(" totalNumberFirst={0},",req.totalNumberFirst);
                        sbsql.AppendFormat(" avePriceFirst={0},", req.avePriceFirst);
                        sbsql.AppendFormat(" waterTotalChargeFirst={0},", req.waterTotalChargeFirst);
                        sbsql.AppendFormat(" totalNumberSencond={0},", req.totalNumberSencond);
                        sbsql.AppendFormat(" avePriceSencond={0},", req.avePriceSencond);
                        sbsql.AppendFormat(" waterTotalChargeSencond={0},", req.waterTotalChargeSencond);
                        sbsql.AppendFormat(" totalNumberThird={0},", req.totalNumberThird);
                        sbsql.AppendFormat(" avePriceThird={0},", req.avePriceThird);
                        sbsql.AppendFormat(" waterTotalChargeThird={0},", req.waterTotalChargeThird);
                        IsSF = (readItem.NoteNo.Substring(0, 1).ToUpper().Equals("A")) ? true : false;

                        //抄表员收费
                        if (waterUserchargeType.Equals("0"))
                        {
                            //如果表本是A开头的，添加一条收费记录
                            if (IsSF)
                            {
                                // sbsql.AppendFormat("chargeID='{0}',", chargeID);
                                // sbsql.AppendFormat("chargeState='{0}',", 3);//在记录添加成功后再修改收费状态
                                sbsql.AppendFormat("checkState='{0}',", 1);
                                sbsql.AppendFormat("checker='{0}',", string.IsNullOrEmpty(req.checker) ? "系统管理员" : req.checker);
                                sbsql.AppendFormat("checkDateTime='{0}',", DateTime.Now.ToString());
                            }
                            else
                            {
                                sbsql.AppendFormat("chargeState='{0}',", 1);
                                sbsql.AppendFormat("checkState='{0}',", 0);
                                sbsql.AppendFormat("checkDateTime='{0}',", DateTime.Now.ToString());
                                // sbsql.AppendFormat("checker='{0}',", string.IsNullOrEmpty(req.checker) ? "admin" : req.checker);
                            }
                        }
                        else
                        {
                            //waterUserchargeType==1营业大厅收费,已抄表，已审核
                            sbsql.AppendFormat("chargeState='{0}',", 1);
                            sbsql.AppendFormat("checkState='{0}',", 1);
                            sbsql.AppendFormat("checker='{0}',", string.IsNullOrEmpty(req.checker) ? "系统管理员" : req.checker);
                            sbsql.AppendFormat("checkDateTime='{0}',", DateTime.Now.ToString());
                        }
                    }
                    else if (readItem.ChaoBiaoTag.Equals("1") && req.chargeState.Equals("3"))
                    {
                        IsSF = true;
                        //chargeID = GetNewChargeID(readItem.loginId);
                        // sbsql.AppendFormat("chargeID='{0}',", chargeID);
                        sbsql.AppendFormat("checkState='{0}',", 1);
                        sbsql.AppendFormat("checker='{0}',", string.IsNullOrEmpty(req.checker) ? "系统管理员" : req.checker);
                        sbsql.AppendFormat("checkDateTime='{0}',", DateTime.Now.ToString());
                        //sbsql.AppendFormat("chargeState='{0}',", 3);//在记录添加成功后再修改收费状态
                    }
                }
                #endregion

                if (OrGpsTag)
                {
                    sbsql.AppendFormat(" Longitude='{0}',", req.Longitude);
                    sbsql.AppendFormat("Latitude='{0}',", req.Latitude);
                }
                if (OrPhoneTag)
                {
                    sbsql.AppendFormat("waterPhone='{0}',", req.Phone);

                }
                if (OrMemoTag)
                {
                    sbsql.AppendFormat("Memo1='{0}'", req.Memo1);

                }
                if (IsChaoBiao || OrGpsTag || OrPhoneTag || OrMemoTag)
                {
                    string SqlTemp=sbsql.ToString().TrimEnd(',');
                    sbsql.Clear();
                    sbsql.Append(SqlTemp);
                    sbsql.Append(" WHERE readMeterRecordId=@readMeterRecordId ");
                   // sbsql.Append("AND chargeState IN (0,1)");
                    context.Sql(sbsql.ToString())
                       .Parameter("readMeterRecordId", req.readMeterRecordId)
                       .Execute();
                }


                if (!IsAllowUpdata)
                {
                    retItem.isErrMsg = true;
                    retItem.errMsg = "数据上传失败：不在上传期限内！";
                    //return retItem;
                }
                if (OrGpsTag)
                {
                    UpdateWaterMeterGPS(req.readMeterRecordId);
                    retItem.isErrMsg = false;
                    retItem.errMsg = "";
                    // return retItem;
                }
                if (OrPhoneTag)
                {
                    sbsql.Clear();
                    sbsql.Append("update wu set wu.waterPhone=rd.waterPhone from readMeterRecord rd join waterUser wu on rd.waterUserNO=wu.waterUserNO ");
                    sbsql.Append(" where rd.readMeterRecordId=@recordId");
                    context.Sql(sbsql.ToString())
                        .Parameter("recordId", req.readMeterRecordId).Execute();
                    retItem.isErrMsg = false;
                    retItem.errMsg = "";
                    // return retItem;
                }
                if (OrMemoTag)
                {
                    sbsql.Clear();
                    sbsql.Append("update wu set wu.Memo1=rd.Memo1 from readMeterRecord rd join waterUser wu on rd.waterUserNO=wu.waterUserNO ");
                    sbsql.Append(" where rd.readMeterRecordId=@recordId");
                    context.Sql(sbsql.ToString())
                        .Parameter("recordId", req.readMeterRecordId).Execute();
                    retItem.isErrMsg = false;
                    retItem.errMsg = "";
                }
                if (IsSF)
                {
                    //添加收费信息
                    InsertChargeFeeInfo(req.readMeterRecordId);
                }


                //if (editPhone)
                //{
                //sbsql.Clear();
                //sbsql.Append("update wu set wu.waterPhone=rd.waterPhone from readMeterRecord rd join waterUser wu on rd.waterUserNO=wu.waterUserNO ");
                //sbsql.Append(" where rd.readMeterRecordId=@recordId");
                //context.Sql(sbsql.ToString())
                //    .Parameter("recordId", req.readMeterRecordId).Execute();
                // }
                return retItem;
            }
        }

        private void InsertChargeFeeInfo(string readMeterRecordId)
        {
            using (var context = WDbContext())
            {
                StringBuilder sbsql = new StringBuilder();
                sbsql.AppendFormat("DECLARE @readMeterRecordId varchar(50)='{0}'\n", readMeterRecordId);
                sbsql.Append("DECLARE @readMeterRecordYear int=0\n");
                sbsql.Append("DECLARE @readMeterRecordMonth int=0\n");
                sbsql.Append("DECLARE @MeterCount int=0\n");//总表数量
                sbsql.Append("DECLARE @waterUserNO varchar(50)\n");//用户号
                sbsql.Append("DECLARE @meterReadingNO varchar(50)\n");
                sbsql.Append("DECLARE @SummaryCount int=0\n");//分表数量
                sbsql.Append("DECLARE @LoginID varchar(10)\n");
                sbsql.Append("Declare @YearMonth varchar(7) \n");
                sbsql.Append("SELECT @readMeterRecordYear=datepart(year,readMeterRecordYearAndMonth),@YearMonth=convert(varchar(7),readMeterRecordYearAndMonth,120),@readMeterRecordMonth=datepart(month,readMeterRecordYearAndMonth),@LoginID=loginId FROM readMeterRecord WHERE readMeterRecordId=@readMeterRecordId\n");
                sbsql.Append("SELECT @waterUserNO=waterUserNO,@meterReadingNO=meterReadingId FROM readMeterRecord where isSummaryMeter=2 and convert(varchar(7),[readMeterRecordYearAndMonth],120)=@YearMonth  AND readMeterRecordId=@readMeterRecordId\n");
                sbsql.Append("and waterUserNO in (select waterUserNO from readMeterRecord where isSummaryMeter=2 and convert(varchar(7),[readMeterRecordYearAndMonth],120)=@YearMonth group by waterUserNO having COUNT(*)>1)\n");
                sbsql.Append("SELECT @MeterCount=COUNT(1) FROM readMeterRecord WHERE isSummaryMeter=2 and convert(varchar(7),[readMeterRecordYearAndMonth],120)=@YearMonth AND waterUserNO=@waterUserNO\n");
                sbsql.Append("SELECT @SummaryCount=COUNT(1) FROM readMeterRecord WHERE  waterMeterParentId=@waterUserNO and convert(varchar(7),[readMeterRecordYearAndMonth],120)=@YearMonth \n");
                sbsql.Append("SELECT @LoginID AS LoginID,@MeterCount AS MeterCount,@readMeterRecordYear AS ReadMeterRecordYear,@readMeterRecordMonth AS ReadMeterRecordMonth,@waterUserNO AS WaterUserNO,@SummaryCount AS SummaryCount\n");

                var retItem = context.Sql(sbsql.ToString()).QuerySingle<WaterMeterArguments>();
                retItem.readMeterRecordId = readMeterRecordId;
                //if (retItem.MeterCount > 1 && retItem.SummaryCount > 0)
                if (retItem.MeterCount > 1)
                {
                    InsertChargeFeeInfo_Multi(retItem);
                }
                else
                {
                    InsertChargeFeeInfo_Single(retItem);
                }
            }
        }

        private void InsertChargeFeeInfo_Multi(WaterMeterArguments readItem)
        {
            string chargeID = GetNewChargeID(readItem.LoginID);
            using (var context = WDbContext())
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("declare @chargeID varchar(50)='{0}'\n", chargeID);
                sql.AppendFormat("declare @readMeterRecordId varchar(50)='{0}'\n", readItem.readMeterRecordId);
                sql.Append("declare @readMeterRecordYear int\n");
                sql.Append("declare @readMeterRecordMonth int\n");
                sql.Append("declare @MeterCount int=0\n");
                sql.Append("declare @waterUserNO varchar(50)\n");
                sql.Append("declare @meterReadingNO varchar(50)\n");
                sql.Append("declare @totalCharge float\n");
                sql.Append("declare @extraCharge1 float\n");
                sql.Append("declare @extraCharge2 float\n");
                sql.Append("declare @waterTotalCharge float\n");
                sql.Append("declare @totalNumber int=0\n");
                sql.Append("declare @totalNumberFB int=0\n");
                sql.Append("declare @totalNumberZB int=0\n");
                sql.Append("declare @OVERDUEMONEY float=0\n");
                sql.Append("declare @avePrice float\n");
                sql.Append("declare @extraChargePrice1 float\n");
                sql.Append("declare @extraChargePrice2 float\n");
                sql.Append("declare @CHARGERID varchar(50)\n");
                sql.Append("Declare @YearMonth varchar(7) \n");
                sql.Append("declare @CHARGEWORKERNAME varchar(50)\n");
                sql.Append("declare @ChargeTemp table (CHARGERID varchar(50),CHARGEWORKERNAME varchar(50))\n");
                sql.Append("SELECT @readMeterRecordYear=datepart(year,readMeterRecordYearAndMonth),@yearmonth=convert(varchar(7),readMeterRecordYearAndMonth,120),@readMeterRecordMonth=datepart(month,readMeterRecordYearAndMonth),@avePrice=avePrice,@extraChargePrice1=extraChargePrice1,@extraChargePrice2=extraChargePrice2 FROM readMeterRecord WHERE readMeterRecordId=@readMeterRecordId\n");
                sql.Append("SELECT @waterUserNO=waterUserNO,@meterReadingNO=meterReadingId FROM readMeterRecord where isSummaryMeter=2 and convert(varchar(7),readMeterRecordYearAndMonth,120)=@yearmonth  AND readMeterRecordId=@readMeterRecordId\n");
                sql.Append("and waterUserNO in (select waterUserNO from readMeterRecord where isSummaryMeter=2 and convert(varchar(7),readMeterRecordYearAndMonth,120)=@yearmonth group by waterUserNO having COUNT(*)>1)\n");
                sql.Append("SELECT @MeterCount=COUNT(1) FROM readMeterRecord WHERE isSummaryMeter=2 and convert(varchar(7),readMeterRecordYearAndMonth,120)=@yearmonth AND waterUserNO=@waterUserNO\n");
                sql.Append("if(@MeterCount>0)\n");
                sql.Append("begin\n");
                sql.Append("SELECT @totalNumberZB=SUM(totalNumber) FROM readMeterRecord WHERE isSummaryMeter=2 and convert(varchar(7),readMeterRecordYearAndMonth,120)=@yearmonth AND waterUserNO=@waterUserNO\n");
                sql.Append("SELECT @totalNumberFB=SUM(totalNumber) FROM readMeterRecord WHERE  waterMeterParentId=@waterUserNO and convert(varchar(7),readMeterRecordYearAndMonth,120)=@yearmonth\n");
                sql.Append("if(@totalNumberFB is null)\n");
                sql.Append("select @totalNumberFB=0\n");
                sql.Append("SELECT @totalNumber=@totalNumberZB-@totalNumberFB\n");
                sql.Append("SELECT @extraCharge1=@totalNumber*@extraChargePrice1\n");
                sql.Append("SELECT @extraCharge2=@totalNumber*@extraChargePrice2\n");
                sql.Append("SELECT @waterTotalCharge=@totalNumber*@avePrice\n");
                sql.Append("SELECT @totalCharge=@extraCharge1+@extraCharge2+@waterTotalCharge\n");
                sql.Append("INSERT INTO @ChargeTemp SELECT (case when CHARGERID is null then loginId else CHARGERID end ) as CHARGERID,(SELECT userName FROM base_login WHERE loginId=(case when MR.CHARGERID is null then MR.loginId else MR.CHARGERID end )) as CHARGEWORKERNAME  FROM meterReading MR  where meterReadingId=@meterReadingNO\n");
                sql.Append("SELECT TOP 1 @CHARGERID=CHARGERID,@CHARGEWORKERNAME=CHARGEWORKERNAME FROM @ChargeTemp\n");
                sql.Append("INSERT INTO OPERATORLOG (LOGTYPE,LOGCONTENT,LOGDATETIME,OPERATORID,MEMO) VALUES ('7','删除收费记录，用户ID：'+@waterUserNO,GETDATE(),@CHARGEWORKERNAME,@readMeterRecordId)\n");
                sql.Append("DELETE WATERFEECHARGE WHERE CHARGEID IN (SELECT CHARGEID FROM readMeterRecord WHERE isSummaryMeter=2 and convert(varchar(7),readMeterRecordYearAndMonth,120)=@yearmonth AND waterUserNO=@waterUserNO)\n");
                sql.Append("INSERT INTO WATERFEECHARGE (CHARGEID,TOTALNUMBERCHARGE,EXTRACHARGECHARGE1,EXTRACHARGECHARGE2,WATERTOTALCHARGE,TOTALCHARGE,OVERDUEMONEY,CHARGETYPEID,CHARGEClASS,CHARGEBCYS,CHARGEBCSS,CHARGEYSQQYE,CHARGEYSBCSZ,CHARGEYSJSYE,CHARGEWORKERID,CHARGEWORKERNAME,CHARGEDATETIME,RECEIPTPRINTCOUNT) VALUES (@chargeID,@totalNumber,@extraCharge1,@extraCharge2,@waterTotalCharge,@totalCharge,@OVERDUEMONEY,'1','1',@totalCharge,@totalCharge,'0','0','0',@CHARGERID,@CHARGEWORKERNAME,GETDATE(),'1')\n");
                sql.Append("INSERT INTO OPERATORLOG (LOGTYPE,LOGCONTENT,LOGDATETIME,OPERATORID,MEMO) VALUES ('7','新增收费记录，记录ID:'+@chargeID,GETDATE(),@CHARGEWORKERNAME,@readMeterRecordId)\n");
                sql.Append("UPDATE readMeterRecord SET chargeID=@chargeID,chargeState=3 WHERE  isSummaryMeter=2 and convert(varchar(7),readMeterRecordYearAndMonth,120)=@yearmonth AND waterUserNO=@waterUserNO AND checkState=1\n");
                //sql.Append("select * from readMeterRecord where readMeterRecordMonth=@readMeterRecordMonth and readMeterRecordYear=@readMeterRecordYear AND waterUserNO=@waterUserNO\n");
                // sql.Append("select * from WATERFEECHARGE where CHARGEID=@chargeID\n");
                sql.Append("end\n");

                context.Sql(sql.ToString()).Execute();

                string sqlDep = " UPDATE WATERFEECHARGE SET DepartmentID = V.departmentID, DepartmentName = V.departmentName FROM WATERFEECHARGE W, V_LOGIN V WHERE W.CHARGEWORKERID=V.loginId AND CHARGEID=@CHARGEID";
                context.Sql(sqlDep)
                    .Parameter("chargeID", chargeID)
                    .Execute();

            }

        }

        private void InsertChargeFeeInfo_Single(WaterMeterArguments readItem)
        {
            var chargeItem = GetMeterDataByReacordID(readItem.readMeterRecordId);
            string CHARGEWORKERNAME, CHARGERID;

            WaterChargeUserRes WCU = new WaterChargeUserRes();

            WCU = GetChargeIDByMeterNo(chargeItem.NoteNo);
            if (WCU != null)
            {
                CHARGERID = WCU.CHARGERID;
                CHARGEWORKERNAME = WCU.CHARGEWORKERNAME;
            }
            else
            {
                CHARGERID = chargeItem.loginId;
                CHARGEWORKERNAME = chargeItem.USERNAME;
            }

            string chargeID = GetNewChargeID(readItem.LoginID);
            using (var context = WDbContext())
            {
                context.Insert("WATERFEECHARGE")
                    .Column("CHARGEID", chargeID)
                    .Column("TOTALNUMBERCHARGE", chargeItem.CurrMonthWNum)//水量
                    .Column("EXTRACHARGECHARGE1", chargeItem.extraCharge1)//污水处理费
                    .Column("EXTRACHARGECHARGE2", chargeItem.extraCharge2)//附加费
                    .Column("WATERTOTALCHARGE", chargeItem.CurrMonthFee)//水费
                    .Column("TOTALCHARGE", chargeItem.TotalCharge)//总水费
                    .Column("OVERDUEMONEY", chargeItem.OVERDUEMONEY)//滞纳金
                    .Column("CHARGETYPEID", 1)//收款方式：1-现金
                    .Column("CHARGEClASS", 1)
                    .Column("CHARGEBCYS", chargeItem.TotalCharge)//本次应收：总水费+滞纳金
                    .Column("CHARGEBCSS", chargeItem.TotalCharge)//本次实收
                    .Column("CHARGEYSQQYE", 0)//前期余额：0
                    .Column("CHARGEYSBCSZ", 0)//本次收支：0
                    .Column("CHARGEYSJSYE", 0)//结算余额：前期余额+本次收支
                    .Column("CHARGEWORKERID", CHARGERID)//收费员ID
                    .Column("CHARGEWORKERNAME", CHARGEWORKERNAME)//收费员名字
                    .Column("CHARGEDATETIME", DateTime.Now.ToString())//收费时间
                    .Column("RECEIPTPRINTCOUNT", 1)//小票打印次数：只允许打印一次，打印代表收费
                    .Column("RECEIPTNO", "")//小票编号：收据编号（可为空）
                    .Column("MEMO", "").Execute();

                string sqlstr = "UPDATE readMeterRecord SET chargeState=3,chargeID=@chargeID,checkState=1,checkDateTime=@checkDateTime,checker='系统管理员' WHERE readMeterRecordId=@readMeterRecordId";
                context.Sql(sqlstr)
                    .Parameter("chargeID", chargeID)
                    .Parameter("readMeterRecordId", readItem.readMeterRecordId)
                    .Parameter("checkDateTime", DateTime.Now.ToString())
                    .Execute();
                // UPDATE WATERFEECHARGE SET DepartmentID = V.departmentID, DepartmentName = V.departmentName FROM WATERFEECHARGE W, V_LOGIN V WHERE W.CHARGEWORKERID=V.loginId AND CHARGEID=''

//                UPDATE WATERFEECHARGE SET DepartmentID = V.departmentID, DepartmentName = V.departmentName 
//,CHARGEYSQQYE=WU.prestore,CHARGEYSJSYE=WU.prestore
//FROM WATERFEECHARGE W, V_LOGIN V ,wateruser WU , readMeterRecord R 
//WHERE W.CHARGEWORKERID=V.loginId AND W.CHARGEID=@CHARGEID AND WU.waterUserId=R.waterUserId AND R.readMeterRecordId=@readMeterRecordId

                //string sqlDep=" UPDATE WATERFEECHARGE SET DepartmentID = V.departmentID, DepartmentName = V.departmentName FROM WATERFEECHARGE W, V_LOGIN V WHERE W.CHARGEWORKERID=V.loginId AND CHARGEID=@CHARGEID";
                //context.Sql(sqlDep)
                //    .Parameter("chargeID", chargeID)
                //    .Execute();

                //2017年5月2日
                string sqlDep = @" UPDATE WATERFEECHARGE SET DepartmentID = V.departmentID, DepartmentName = V.departmentName ,CHARGEYSQQYE=WU.prestore,CHARGEYSJSYE=WU.prestore
                                   FROM WATERFEECHARGE W, V_LOGIN V ,wateruser WU , readMeterRecord R 
                                   WHERE W.CHARGEWORKERID=V.loginId AND W.CHARGEID=@CHARGEID AND WU.waterUserId=R.waterUserId AND R.readMeterRecordId=@readMeterRecordId";
                context.Sql(sqlDep)
                    .Parameter("chargeID", chargeID)
                     .Parameter("readMeterRecordId", readItem.readMeterRecordId)
                    .Execute();

                //判断是不是两块总表的分表，如果是需要修改总表的费用，找出总表的readMeterRecordId,重新执行InsertChargeFeeInfo(string readMeterRecordId)
                CheckBranch(readItem.readMeterRecordId);
            }
        }

        private void CheckBranch(string readMeterRecordId)
        {
            using (var context = WDbContext())
            {
                StringBuilder sbsql = new StringBuilder();
                sbsql.AppendFormat("DECLARE @readMeterRecordId varchar(50)='{0}'\n", readMeterRecordId);
                sbsql.AppendFormat("DECLARE @waterMeterParentId varchar(50);\n");
                sbsql.AppendFormat("DECLARE @readMeterRecordYear int\n");
                sbsql.AppendFormat("DECLARE @readMeterRecordMonth int\n");
                sbsql.AppendFormat("DECLARE @MainreadMeterRecordId varchar(50)\n");
                sbsql.AppendFormat("DECLARE @MeterCount int=0\n");
                sbsql.AppendFormat("declare @yearmonth varchar(7)\n");
                sbsql.AppendFormat("SELECT @readMeterRecordYear=readMeterRecordYear,@yearmonth=convert(varchar(7),readMeterRecordYearAndMonth,120),@readMeterRecordMonth=readMeterRecordMonth FROM readMeterRecord WHERE readMeterRecordId=@readMeterRecordId\n");
                sbsql.AppendFormat("SELECT @waterMeterParentId=waterMeterParentId FROM readMeterRecord WHERE readMeterRecordId=@readMeterRecordId\n");
                sbsql.AppendFormat("SELECT @MeterCount=COUNT(1) FROM readMeterRecord WHERE isSummaryMeter=2 and convert(varchar(7),readMeterRecordYearAndMonth,120)=@yearmonth AND waterUserNO=@waterMeterParentId\n");
                sbsql.AppendFormat("if(@MeterCount>0)\n");
                sbsql.AppendFormat("SELECT TOP 1 @MainreadMeterRecordId=readMeterRecordId FROM readMeterRecord WHERE isSummaryMeter=2 and convert(varchar(7),readMeterRecordYearAndMonth,120)=@yearmonth AND waterUserNO=@waterMeterParentId\n");
                sbsql.AppendFormat("SELECT @MeterCount AS MeterCount,@MainreadMeterRecordId AS MainreadMeterRecordId\n");

                var retItem = context.Sql(sbsql.ToString()).QuerySingle<WaterMeterArguments>();
                if (retItem.MeterCount > 0)
                {
                    InsertChargeFeeInfo(retItem.MainreadMeterRecordId);
                }

            }
        }

        private string GetNewChargeID(string loginid)
        {
            using (var context = WDbContext())
            {
                int index = 1;
                string chargeID = string.Format("{0}{1}SF{2}", DateTime.Now.ToString("yyyyMMdd"), loginid, String.Format("{0:000000}", index));
                //string strSql = "SELECT top 1 right(chargeID,6) AS chargeID  FROM readMeterRecord where loginId=@LOGINID and chargeState=3 and chargeID<>'' and  convert(char(10),checkDateTime,120)=DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), 0) order by right(chargeID,6) desc";
                string strSql = "SELECT TOP 1 right(chargeID,6) AS chargeID FROM WATERFEECHARGE WHERE convert(char(10),LEFT(CHARGEID,8),120)=DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), 0) AND SUBSTRING(CHARGEID,9,4)=@LOGINID ORDER BY right(chargeID,6) desc";
                var retItem = context.Sql(strSql)
                    .Parameter("LOGINID", loginid)
                    .QuerySingle<ChargeItemsres>();
                if (retItem != null)
                {
                    if (!string.IsNullOrEmpty(retItem.chargeID))
                    {
                        index = int.Parse(retItem.chargeID) + 1;
                        chargeID = string.Format("{0}{1}SF{2}", DateTime.Now.ToString("yyyyMMdd"), loginid, String.Format("{0:000000}", index));
                    }
                }
                return chargeID;
            }
        }

        public VersionInfoRes GetVersionInfo(VersionInfoReq req)
        {
            using (var context = WDbContext())
            {
                string strSql = @"select top 1 versionCode,AppPath as fileUrl,updateInfo from tb_appversion
                                  order by Adddate desc";
                var retItem = context.Sql(strSql).QuerySingle<VersionInfoRes>();
                return retItem;
            }
        }

        public WPriceRes GetPriceInfo(WPriceReq req)
        {
            using (var context = WDbContext())
            {
                string strSql = "select SIndex,WFrom,WTo,WPrice,PriceType,PriceTypeName from tb_UnitPrice ";
                var retItem = context.Sql(strSql).QueryMany<WPriceItem>();
                WPriceRes res = new WPriceRes();
                res.priceItems = retItem;
                return res;
            }
        }

        public WPointRes AddTrack(WPointReq req)
        {
            var retRes = new WPointRes();
            StringBuilder sbuilderSql = new StringBuilder();
            using (var context = WDbContext())
            {
                if (req.pointItems != null && req.pointItems.Count >= 0)
                {
                    sbuilderSql.Append("INSERT INTO UserTrack(LoginId,CreateDateTime,Latitude,Longitude,section)VALUES");
                    foreach (var itm in req.pointItems)
                    {
                        sbuilderSql.Append("('" + itm.UserId + "','" + itm.AddDate + "','" + itm.Latitude + "','" + itm.Longitude + "'," + 0 + "),");
                    }
                }
                string strSql = sbuilderSql.ToString().Trim(',');
                if (!string.IsNullOrEmpty(strSql))
                {
                    context.Sql(strSql).Execute();
                }
            }
            return retRes;
        }
        public WAdviceRes AddUserAdvice(WAdviceReq req)
        {
            var retRes = new WAdviceRes();
            StringBuilder sbuilderSql = new StringBuilder();
            using (var context = WDbContext())
            {
                if (req.adviceItems != null && req.adviceItems.Count > 0)
                {
                    sbuilderSql.Append("INSERT INTO UserSuggest(waterUserId,USContent,CreateDateTime,LoginId)VALUES");
                    foreach (var itm in req.adviceItems)
                    {
                        sbuilderSql.Append("('" + itm.UserNo + "','" + itm.Advice + "','" + itm.AddDate + "','" + itm.AddUserId + "'),");
                    }
                    string strSql = sbuilderSql.ToString().Trim(',');
                    if (!string.IsNullOrEmpty(strSql))
                    {
                        context.Sql(strSql).Execute();
                    }
                }

            }
            return retRes;
        }
        public WFaultReportRes AddFaultReport(WFaultReportReq req)
        {
            WFaultReportRes resItem = new WFaultReportRes();
            using (var context = WDbContext())
            {
                byte[] imgData = new byte[0];
                try
                {
                    imgData = Convert.FromBase64String(req.ImgData);
                }
                catch { }
                context.Insert("MeterRepair")
                    .Column("waterUserId", req.WaterUserId)
                    .Column("Describe", req.Describe)
                    .Column("CreateDateTime", req.CreateDateTime)
                    .Column("LoginId", req.LoginId)
                    .Column("Images", imgData).Execute();
            }
            return resItem;
        }
        public void UpdateWaterMeterGPS(string readMeterRecordId)
        {
            using (var context = WDbContext())
            {
                string strSql = "UPDATE W SET Longitude=R.Longitude,Latitude=R.Latitude FROM waterMeter W join readMeterRecord R on W.waterMeterId=R.waterMeterId where R.readMeterRecordId=@readMeterRecordId";
                context.Sql(strSql)
                    .Parameter("readMeterRecordId", readMeterRecordId)
                    .Execute();
            }
        }

        //废弃
        public WaterChargeRes AddChargeItem(CharegeReq req)
        {
            WaterChargeRes resItem = new WaterChargeRes();
            using (var context = WDbContext())
            {
                var chargeItem = req.chageItem;
                if (chargeItem == null)
                {
                    resItem.errMsg = "";
                    resItem.errMsgNo = 1;
                    resItem.isErrMsg = true;
                    resItem.ExecResult = 0;
                    return resItem;
                }

                WaterChargeUserRes WUR = GetLoginIDByChargeID(chargeItem.CHARGEID);

                string ChargeUserID = chargeItem.CHARGEWORKERID;
                string ChargeUser = chargeItem.CHARGEWORKERNAM;
                string ChargeDate = chargeItem.CHARGEDATETIME;

                string LoginID = WUR.loginId;

                if (string.IsNullOrEmpty(ChargeUserID))
                {
                    ChargeUserID = WUR.CHARGERID;
                    ChargeUser = WUR.CHARGEWORKERNAME;
                }
                if (string.IsNullOrEmpty(ChargeDate))
                {
                    ChargeDate = DateTime.Now.ToString();
                }

                //resItem.ExecResult = context.Insert("WATERFEECHARGE")
                //    .Column("CHARGEID", chargeItem.CHARGEID)
                //    .Column("TOTALNUMBERCHARGE", chargeItem.TOTALNUMBERCHARGE)
                //    .Column("EXTRACHARGECHARGE1", chargeItem.EXTRACHARGECHARGE1)
                //    .Column("EXTRACHARGECHARGE2", chargeItem.EXTRACHARGECHARGE2)
                //    .Column("WATERTOTALCHARGE", chargeItem.WATERTOTALCHARGE)
                //    .Column("TOTALCHARGE", chargeItem.TOTALCHARGE)
                //    .Column("OVERDUEMONEY", chargeItem.OVERDUEMONEY)
                //    .Column("CHARGETYPEID", chargeItem.CHARGETYPEID)
                //    .Column("CHARGEClASS", chargeItem.CHARGEClASS)
                //    .Column("CHARGEBCYS", chargeItem.CHARGEBCYS)
                //    .Column("CHARGEBCSS", chargeItem.CHARGEBCSS)
                //    .Column("CHARGEYSQQYE", chargeItem.CHARGEYSQQYE)
                //    .Column("CHARGEYSBCSZ", chargeItem.CHARGEYSBCSZ)
                //    .Column("CHARGEYSJSYE", chargeItem.CHARGEYSJSYE)
                //    .Column("CHARGEWORKERID", ChargeUserID)//收费员ID
                //    .Column("CHARGEWORKERNAME", ChargeUser)//收费员名字
                //    .Column("CHARGEDATETIME", ChargeDate)//收费时间
                //    .Column("RECEIPTPRINTCOUNT", chargeItem.RECEIPTPRINTCOUNT)
                //    .Column("RECEIPTNO", chargeItem.RECEIPTNO)
                //    .Column("MEMO", chargeItem.MEMO).Execute();
            }
            return resItem;
        }
        //获取最后一条收费ID序号
        public ChargeItemsres GetChargeItemByLoginID(WUserItemsReq req)
        {
            using (var context = WDbContext())
            {
                string strSql = "SELECT top 1 right(chargeID,6) AS chargeID  FROM readMeterRecord where loginId=@LOGINID and chargeState=3 and chargeID<>'' and  convert(char(10),checkDateTime,120)=DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), 0) order by right(chargeID,6) desc";
                var retItem = context.Sql(strSql)
                    .Parameter("LOGINID", req.loginid)
                    .QuerySingle<ChargeItemsres>();
                return retItem;
            }
        }

        public QianFeiItemRes getQainFeiItemRes(QianFeiItemReq req)
        {
            using (var context = WDbContext())
            {
                string strSql = "SELECT waterUserId,waterUserNO,waterUserName,waterPhone,waterUserAddress,meterReadingID meterReadingNO,prestore,TOTALFEE,TOTALNUMBER,ordernumber FROM V_WATERUSERAREARAGE where loginId=@loginId and (([prestore]-[TOTALFEE])<0)";
                if (req.meterReadingNO == "0" || string.IsNullOrEmpty(req.meterReadingNO))
                {
                    strSql += " and meterReadingID<>@meterReadingNO";
                }
                else
                {
                    strSql += " and meterReadingID=@meterReadingNO";
                }
                var retItem = context.Sql(strSql)
                     .Parameter("loginId", req.loginId)
                    .Parameter("meterReadingNO", req.meterReadingNO)
                    .QueryMany<QianFeiItem>();

                QianFeiItemRes res = new QianFeiItemRes();
                res.QianFeiItem = retItem;
                return res;
            }
        }

        public ServerTimeRes getServerTimes()
        {
            ServerTimeRes res = new ServerTimeRes();

            // res.ServerTime = string.Format("{0}-{1}-{2}", DateTime.Now.Year.ToString(),DateTime.Now.Month.ToString().PadLeft(2,'0'),DateTime.Now.Day.ToString().PadLeft(2,'0'));
            res.ServerTime = DateTime.Now.ToString("yyyy-MM-dd");

            return res;
        }

        public QianFeiHistoryItemRes getQainFeiHistoryItemRes(QianFeiItemReq req)
        {
            using (var context = WDbContext())
            {
                string strSql = "SELECT waterUserId,waterUserNO,waterUserName,waterPhone,waterUserAddress,waterMeterLastNumber,waterMeterEndNumber,totalNumber,totalCharge,datepart(year,readMeterRecordYearAndMonth) readMeterRecordYear,datepart(month,readMeterRecordYearAndMonth) readMeterRecordMonth,meterReadingNO,meterReadingID,ordernumber,WATERUSERQQYE,WATERUSERJSYE,INFORMPRINTSIGN FROM V_YSDETAIL_BYWATERMETER where loginId=@loginId and chargeState=1 and checkState=1 and INFORMPRINTSIGN=1";
                if (req.meterReadingNO == "0" || string.IsNullOrEmpty(req.meterReadingNO))
                {
                    strSql += " and meterReadingId<>@meterReadingNO";
                }
                else
                {
                    strSql += " and meterReadingId=@meterReadingNO";
                }
                var retItem = context.Sql(strSql)
                     .Parameter("loginId", req.loginId)
                    .Parameter("meterReadingNO", req.meterReadingNO)
                    .QueryMany<QianFeiHistoryItem>();

                QianFeiHistoryItemRes res = new QianFeiHistoryItemRes();
                res.QianFeiHistoryItem = retItem;
                return res;
            }
        }

        public BaseRes UpdateInvoiceInfo(InvoiceReq req)
        {
            BaseRes res = new BaseRes();
            if (req == null|string.IsNullOrWhiteSpace(req.invoiceNo)||string.IsNullOrWhiteSpace(req.readMeterId))
            {
                res.errMsg = "参数错误";
                res.errMsgNo = -1;
                return res;
            }
            res.errMsg = "";
            res.errMsgNo = 0;
            using (var context = WDbContext())
            {
                string strSql = @"declare @chargeId nvarchar(50)=''
                                select @chargeId=mr.chargeID from readMeterRecord mr with(nolock)
                                where mr.readMeterRecordId=@reacordId
                                if(@chargeId is not null and @chargeId<>'')
                                begin
                                  if not exists(select 1 from CHARGEINVOICEPRINT cp with(nolock) where cp.CHARGEID=@chargeId)
                                  begin
                                      insert into CHARGEINVOICEPRINT(CHARGEINVOICEPRINTID,CHARGEID,INVOICENO,INVOICEPRINTDATETIME,INVOICEPRINTWORKERID,INVOICEPRINTWORKERNAME,readMeterRecordYearAndMonth,
	                                  waterUserName,waterMeterNo,waterUserAddress,waterMeterTypeId,waterMeterTypeName,waterMeterLastNumber,waterMeterEndNumber,totalNumber,waterTotalCharge,extraCharge1,extraCharge2,totalCharge)
	                                  select @chargeId,@chargeId,@invoiceNo,getdate(),rm.loginId,rm.USERNAME,rm.lastNumberYearMonth,rm.waterUserName,rm.waterUserNO,rm.waterUserAddress,
	                                  rm.waterMeterTypeId,rm.waterMeterTypeName,rm.waterMeterLastNumber,rm.waterMeterEndNumber,rm.totalNumber,rm.waterTotalCharge,rm.extraCharge1,rm.extraCharge2,rm.totalCharge
	                                  from readMeterRecord rm where rm.chargeID=@chargeId
                                  end
                                end";
                context.Sql(strSql)
                    .Parameter("reacordId", req.readMeterId)
                    .Parameter("invoiceNo", req.invoiceNo)
                    .Execute();
            }
            return res;
        }

        public LoginRes getAllowUpdateDate(WBBReq req)
        {
            using (var context = WDbContext())
            {
                string strSql = "SELECT MeterDateTimeBegin,MeterDateTimeEnd FROM base_login WHERE LOGINID=@loginId";
                var retItem = context.Sql(strSql)
                        .Parameter("loginId", req.LoginID)
                       .QuerySingle<LoginRes>();
                return retItem;
            }
        }
        //计算水价

        /// <summary>
        /// 根据阶梯水价字符串及抄表水量计算水费与平均单价
        /// </summary>
        /// <param name="decUsedTotalNum">本年已经使用的水量</param>
        /// <param name="decTotalNumber">本次抄表水量</param>
        /// <param name="strTrapePriceString">阶梯水价字符串</param>
        /// <returns></returns>
        public List<decimal> GetAvePrice(decimal decUsedTotalNum, decimal decTotalNumber, string strTrapePriceString)
        {
            List<decimal> retList = new List<decimal>();
            string[] strComputeTrape = new string[3];
            //string strTrapePriceString="0-32:1.52|32-50:2.3|50-80:3.5|80-100:4.5|100-120:6";
            string[] strTrapePrice = strTrapePriceString.Split('|');
            decimal decWaterSum = 0, decWaterTotalNumber = decTotalNumber;
            decTotalNumber = decTotalNumber + decUsedTotalNum;
            for (int i = strTrapePrice.Length - 1; i >= 0; i--)
            {
                string[] strJTAndPrice = strTrapePrice[i].Split(':');
                string[] strJT = strJTAndPrice[0].Split('-');
                if (IsNumeric(strJT[0]) && IsNumeric(strJT[1]))
                {
                    if (decTotalNumber > Convert.ToDecimal(strJT[0]) && decTotalNumber <= Convert.ToDecimal(strJT[1]))
                    {
                        if (decUsedTotalNum >= Convert.ToDecimal(strJT[0]))
                        {
                            decWaterSum += (decTotalNumber - decUsedTotalNum) * (Convert.ToDecimal(strJTAndPrice[1]));
                            if (strComputeTrape[1] != null)
                                strComputeTrape[1] += "+(" + decTotalNumber.ToString() + "-" + decUsedTotalNum.ToString() + ")*" + strJTAndPrice[1];
                            else
                                strComputeTrape[1] += "计算过程:(" + decTotalNumber.ToString() + "-" + decUsedTotalNum.ToString() + ")*" + strJTAndPrice[1];
                            break;
                        }
                        else
                        {
                            decWaterSum += (decTotalNumber - Convert.ToDecimal(strJT[0])) * (Convert.ToDecimal(strJTAndPrice[1]));
                            if (strComputeTrape[1] != null)
                                strComputeTrape[1] += "+(" + decTotalNumber.ToString() + "-" + strJT[0] + ")*" + strJTAndPrice[1];
                            else
                                strComputeTrape[1] += "计算过程:(" + decTotalNumber.ToString() + "-" + strJT[0] + ")*" + strJTAndPrice[1];
                        }

                        decTotalNumber = Convert.ToDecimal(strJT[0]);
                    }
                    else
                        continue;
                }
            }
            if (decWaterTotalNumber > 0)
                strComputeTrape[0] = (decWaterSum / decWaterTotalNumber).ToString("f3");
            else
                strComputeTrape[0] = "0";
            strComputeTrape[1] += "=" + decWaterSum.ToString() + "÷" + decWaterTotalNumber.ToString() + "=" + strComputeTrape[0];
            strComputeTrape[2] = decWaterSum.ToString("F2");
            retList.Add(decimal.Parse(strComputeTrape[0]));
            retList.Add(decimal.Parse(strComputeTrape[2]));
            return retList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="decUsedTotalNum"></param>
        /// <param name="decTotalNumber"></param>
        /// <param name="strTrapePriceString"></param>
        /// <returns></returns>
        public string[] GetAllAvePrice(decimal decUsedTotalNum, decimal decTotalNumber, string strTrapePriceString)
        {
            string[] strComputeTrape = new string[12];//0:平均单价 1:水费小计计算过程 2:水费小计 3-11:三级阶梯单价3 6 9/水量4 7 10/金额5 8 11
            //string strTrapePriceString="0-32:1.52|32-50:2.3|50-80:3.5|80-100:4.5|100-120:6";

            for (int j = 3; j < strComputeTrape.Length; j++)
                strComputeTrape[j] = "0";

            string[] strTrapePrice = strTrapePriceString.Split('|');
            decimal decWaterSum = 0, decWaterTotalNumber = decTotalNumber;
            decTotalNumber = decTotalNumber + decUsedTotalNum;
            for (int i = strTrapePrice.Length - 1; i >= 0; i--)
            {
                string[] strJTAndPrice = strTrapePrice[i].Split(':');
                string[] strJT = strJTAndPrice[0].Split('-');
                if (IsNumeric(strJT[0]) && IsNumeric(strJT[1]))
                {
                    if (decTotalNumber > Convert.ToDecimal(strJT[0]) && decTotalNumber <= Convert.ToDecimal(strJT[1]))
                    {
                        if (decUsedTotalNum >= Convert.ToDecimal(strJT[0]))
                        {
                            strComputeTrape[3 + i * 3] = strJTAndPrice[1];
                            strComputeTrape[4 + i * 3] = (decTotalNumber - decUsedTotalNum).ToString();
                            strComputeTrape[5 + i * 3] = ((decTotalNumber - decUsedTotalNum) * (Convert.ToDecimal(strJTAndPrice[1]))).ToString();
                            decWaterSum += (decTotalNumber - decUsedTotalNum) * (Convert.ToDecimal(strJTAndPrice[1]));
                            if (strComputeTrape[1] != null)
                                strComputeTrape[1] += "+(" + decTotalNumber.ToString() + "-" + decUsedTotalNum.ToString() + ")*" + strJTAndPrice[1];
                            else
                                strComputeTrape[1] += "计算过程:(" + decTotalNumber.ToString() + "-" + decUsedTotalNum.ToString() + ")*" + strJTAndPrice[1];
                            break;
                        }
                        else
                        {
                            strComputeTrape[3 + i * 3] = strJTAndPrice[1];
                            strComputeTrape[4 + i * 3] = (decTotalNumber - Convert.ToDecimal(strJT[0])).ToString();
                            strComputeTrape[5 + i * 3] = ((decTotalNumber - Convert.ToDecimal(strJT[0])) * (Convert.ToDecimal(strJTAndPrice[1]))).ToString();
                            decWaterSum += (decTotalNumber - Convert.ToDecimal(strJT[0])) * (Convert.ToDecimal(strJTAndPrice[1]));
                            if (strComputeTrape[1] != null)
                                strComputeTrape[1] += "+(" + decTotalNumber.ToString() + "-" + strJT[0] + ")*" + strJTAndPrice[1];
                            else
                                strComputeTrape[1] += "计算过程:(" + decTotalNumber.ToString() + "-" + strJT[0] + ")*" + strJTAndPrice[1];
                        }

                        decTotalNumber = Convert.ToDecimal(strJT[0]);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            if (decWaterTotalNumber > 0)
                strComputeTrape[0] = (decWaterSum / decWaterTotalNumber).ToString("f3");
            else
                strComputeTrape[0] = "0";
            strComputeTrape[1] += "=" + decWaterSum.ToString() + "÷" + decWaterTotalNumber.ToString() + "=" + strComputeTrape[0];
            strComputeTrape[2] = decWaterSum.ToString("F2");
            return strComputeTrape;
        }


        private bool IsNumeric(string testStr)
        {
            if (string.IsNullOrWhiteSpace(testStr))
            {
                return false;
            }
            var retData = 0m;
            if (Decimal.TryParse(testStr, out retData))
            {
                return true;
            }
            return false;
        }
        public decimal getTotleCount(string waterUserId)
        {
            decimal decUsedTotalNumber = 0;
            string strMeterRecordYearAndMonth = DateTime.Now.ToShortDateString();
            if (string.IsNullOrWhiteSpace(waterUserId))
            {
                return decUsedTotalNumber;
            }
            using (var context = WDbContext())
            {
                string strGetUsedTotalNumber = "SELECT SUM(TOTALNUMBER) AS TOTALNUMBER FROM readMeterRecord " +
                        "WHERE DATEDIFF(YEAR,readMeterRecordDate,'" + strMeterRecordYearAndMonth + "')=0 AND checkState='1' AND chargeState<>'0' " +
                        "AND waterUserId='" + waterUserId + "'";
                decUsedTotalNumber = context.Sql(strGetUsedTotalNumber)
                    .QuerySingle<decimal>();
            }
            return decUsedTotalNumber;

        }

        /// <summary>
        /// 获取阶梯水价
        /// </summary>
        /// <returns></returns>
        public string GetTrapePriceString(string readMeterRecordID)
        {
            string retStr = "";
            if (string.IsNullOrWhiteSpace(readMeterRecordID))
            {
                return retStr;
            }
            using (var context = WDbContext())
            {
                string strSql = @"select top 1 trapezoidPrice from readMeterRecord with(nolock)
                                where readMeterRecordId = @recordId";
                retStr = context.Sql(strSql)
                    .Parameter("recordId", readMeterRecordID)
                    .QuerySingle<string>();
                return retStr;
            }
        }

        //走收打票收费
        public WUploadUserRes GetSingleFeeItemRes(WSingleUserItemReq SingleUserFeeItem)
        {
            var retItem = new WUploadUserRes();
            retItem.errMsg = "";
            retItem.isErrMsg = false;
            string err = "";

            #region 是否允许上传
            var readItem = GetMeterDataByReacordID(SingleUserFeeItem.readMeterRecordId);

            int CurrentMonth = int.Parse(DateTime.Now.Month.ToString());
            bool IsAllowUpdata = true;
            if (!IsUploadExtent(readItem.loginId))
            {
                IsAllowUpdata = false;
                err = "数据上传失败：不在上传期限内！";
            }
            if (CurrentMonth != readItem.ReadMeterRecordMonth)
            {
                IsAllowUpdata = false;
                err = "数据上传失败：超出上传月份！";
            }
            if (readItem.TotalCharge == 0)
            {
                IsAllowUpdata = false;
                err = "数据上传失败：用户收费失败！";
            }
            #endregion
            if (IsAllowUpdata)
            {
                InsertChargeFeeInfo(SingleUserFeeItem.readMeterRecordId);
            }
            else
            {
                retItem.isErrMsg = true;
                retItem.errMsg = err;
            }
            //var chargeItem = GetMeterDataByReacordID(SingleUserFeeItem.readMeterRecordId);
            //string CHARGEWORKERNAME, CHARGERID;

            //WaterChargeUserRes WCU = new WaterChargeUserRes();

            //WCU = GetChargeIDByMeterNo(chargeItem.NoteNo);
            //if (WCU != null)
            //{
            //    CHARGERID = WCU.CHARGERID;
            //    CHARGEWORKERNAME = WCU.CHARGEWORKERNAME;
            //}
            //else
            //{
            //    CHARGERID = chargeItem.loginId;
            //    CHARGEWORKERNAME = chargeItem.USERNAME;
            //}

            //string chargeID = GetNewChargeID(chargeItem.loginId);
            //using (var context = WDbContext())
            //{
            //    try
            //    {
            //        context.Insert("WATERFEECHARGE")
            //       .Column("CHARGEID", chargeID)
            //       .Column("TOTALNUMBERCHARGE", chargeItem.CurrMonthWNum)//水量
            //       .Column("EXTRACHARGECHARGE1", chargeItem.extraCharge1)//污水处理费
            //       .Column("EXTRACHARGECHARGE2", chargeItem.extraCharge2)//附加费
            //       .Column("WATERTOTALCHARGE", chargeItem.CurrMonthFee)//水费
            //       .Column("TOTALCHARGE", chargeItem.TotalCharge)//总水费
            //       .Column("OVERDUEMONEY", chargeItem.OVERDUEMONEY)//滞纳金
            //       .Column("CHARGETYPEID", 1)//收款方式：1-现金
            //       .Column("CHARGEClASS", 1)
            //       .Column("CHARGEBCYS", chargeItem.TotalCharge)//本次应收：总水费+滞纳金
            //       .Column("CHARGEBCSS", chargeItem.TotalCharge)//本次实收
            //       .Column("CHARGEYSQQYE", 0)//前期余额：0
            //       .Column("CHARGEYSBCSZ", 0)//本次收支：0
            //       .Column("CHARGEYSJSYE", 0)//结算余额：前期余额+本次收支
            //       .Column("CHARGEWORKERID", CHARGERID)//收费员ID
            //       .Column("CHARGEWORKERNAME", CHARGEWORKERNAME)//收费员名字
            //       .Column("CHARGEDATETIME", DateTime.Now.ToString())//收费时间
            //       .Column("RECEIPTPRINTCOUNT", 1)//小票打印次数：只允许打印一次，打印代表收费
            //       .Column("RECEIPTNO", "")//小票编号：收据编号（可为空）
            //       .Column("MEMO", "").Execute();

            //        string sqlstr = "UPDATE readMeterRecord SET chargeState=3,chargeID=@chargeID WHERE readMeterRecordId=@readMeterRecordId";
            //        context.Sql(sqlstr)
            //            .Parameter("chargeID", chargeID)
            //            .Parameter("readMeterRecordId", SingleUserFeeItem.readMeterRecordId)
            //            .Execute();
            //    }
            //    catch (Exception)
            //    {
            //        res.isErrMsg = true;
            //        res.errMsg = "收费数据上传失败， 请重新上传！";
            //    }
            return retItem;
            //}
        }

        #region
        public WaterChargeUserRes GetLoginIDByChargeID(string CHARGEID)
        {
            using (var context = WDbContext())
            {
                string strSql = "SELECT *  FROM V_SearchAreaRage WHERE CHARGEID=@CHARGEID";
                var retItem = context.Sql(strSql)
                    .Parameter("CHARGEID", CHARGEID)
                    .QuerySingle<WaterChargeUserRes>();
                return retItem;
            }
        }

        public WaterChargeUserRes GetChargeIDByMeterNo(string meterReadingNO)
        {
            using (var context = WDbContext())
            {
                string strSql = "SELECT meterReadingNO ,meterReadingNO AS CHARGEID ,loginId , (case when CHARGERID is null then loginId else CHARGERID end ) as CHARGERID,AREAID,(SELECT userName FROM base_login WHERE loginId=(case when MR.CHARGERID is null then MR.loginId else MR.CHARGERID end )) as CHARGEWORKERNAME  FROM meterReading MR  where meterReadingId=@meterReadingNO";
                var retItem = context.Sql(strSql)
                    .Parameter("meterReadingNO", meterReadingNO)
                    .QuerySingle<WaterChargeUserRes>();
                return retItem;
            }
        }

        #endregion

        public WChargeInfoRes GetChargeInfoItemRes(WChargeInfoReq req)
        {
            using (var context = WDbContext())
            {
                //判断当天/当月
//                --当天
//SELECT COUNT(DISTINCT WATERUSERID) AS 用户数量,SUM(TOTALNUMBERCHARGE) AS 水量合计,SUM(CHARGEBCSS) AS 收费合计 FROM V_WATERFEECHARGE_READMETERRECORD
//WHERE DATEDIFF(DAY,CHARGEDATETIME,GETDATE())=0 AND CHARGEWORKERID='收费员ID'

//--当月
//SELECT COUNT(DISTINCT WATERUSERID) AS 用户数量,SUM(TOTALNUMBERCHARGE) AS 水量合计,SUM(CHARGEBCSS) AS 收费合计 FROM V_WATERFEECHARGE_READMETERRECORD
//WHERE DATEDIFF(MONTH,CHARGEDATETIME,GETDATE())=0 AND CHARGEWORKERID='收费员ID'
                string strSql = "";
                if (req.TJType)
                {
                    strSql = @"SELECT COUNT(DISTINCT WATERUSERID) AS ChargeCount,SUM(TOTALNUMBERCHARGE) AS ChargeWater,SUM(CHARGEBCSS) AS ChargeFee FROM V_WATERFEECHARGE_READMETERRECORD
WHERE DATEDIFF(MONTH,CHARGEDATETIME,GETDATE())=0 AND CHARGEWORKERID=@loginId";
                }
                else
                {
                    strSql = @"SELECT COUNT(DISTINCT WATERUSERID) AS ChargeCount,SUM(TOTALNUMBERCHARGE) AS ChargeWater,SUM(CHARGEBCSS) AS ChargeFee FROM V_WATERFEECHARGE_READMETERRECORD
WHERE DATEDIFF(DAY,CHARGEDATETIME,GETDATE())=0 AND CHARGEWORKERID=@loginId";
                }


                var retItem = context.Sql(strSql)
                     .Parameter("loginId", req.loginid)
                     .QuerySingle<WChargeInfoRes>();
                    //.QueryMany<ChargeInfoItem>();
                //WChargeInfoRes res = new WChargeInfoRes();
                //res.chargeItem = retItem;
                return retItem;
            }
        }

        #region //RONG 2016-4-30

        public class WaterChargeUserRes : BaseRequest
        {
            //[meterReadingNO] 表本号
            //[loginId] 抄表员ID
            //[CHARGERID]收费员ID
            //[AREAID]区域ID
            //CHARGEWORKERNAME收费员名字
            public string meterReadingNO { get; set; }
            public string loginId { get; set; }
            public string CHARGEID { get; set; }
            public string areaId { get; set; }
            public string CHARGERID { get; set; }
            public string CHARGEWORKERNAME { get; set; }

        }

        public class WaterUserType : BaseRequest
        {
            public string readMeterRecordId { get; set; }//主键
            public string meterReadingNO { get; set; }//表本号
            public string waterUserTypeId { get; set; }//用水性质
        }

        public class WaterMeterArguments
        {
            //DECLARE @readMeterRecordId varchar(50)='20160525000250'
            //DECLARE @readMeterRecordYear int
            //DECLARE @readMeterRecordMonth int
            //DECLARE @MeterCount int=0--总表数量
            //DECLARE @waterUserNO varchar(50)--用户数
            //DECLARE @meterReadingNO varchar(50)--抄表本
            //DECLARE @SummaryCount int=0--分表数量
            //DECLARE @MeterOverCount int=0--总表抄完数量
            //DECLARE @SummaryOverCount int=0--分表抄完数量
            //DECLARE @SummaryOver bit=0--分表是否抄完
            //DECLARE @MeterOver bit=0--总表是否抄完
            public string readMeterRecordId { get; set; }
            public string LoginID { get; set; }
            public int MeterCount { get; set; }
            public int ReadMeterRecordYear { get; set; }
            public int ReadMeterRecordMonth { get; set; }
            public string WaterUserNO { get; set; }
            public int SummaryCount { get; set; }
            public string MainreadMeterRecordId { get; set; }
        }

        #endregion


       
    }
}