using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using TestAndroid.Models.Entity;
using TestAndroid.Models.Request;
using TestAndroid.Models.Response;
using TestAndroid.DAL;
using MeterAPPServer.Models.Response;
using MeterAPPServer.Models.Request;
using MeterAPPServer.Models.Entity;
namespace TestAndroid.BLL
{
    public class AndroidOperateService
    {
        CbSystemDAL cbDal = new CbSystemDAL();
        //注册手机
        public EquipmentRes Reg(EquipmentReg req)
        {
            var logRes = cbDal.Reg(req);
            return logRes;
        }
        //登录信息
        public LoginRes Login(LoginReq req)
        {
            var logRes = cbDal.Login(req);
            return logRes;
        }
        //表本信息
        public WBBRes GetBBInfo(WBBReq bbreq)
        {
            WBBRes res = cbDal.GetBBInfo(bbreq);
            if (res == null)
            {
                res = new WBBRes();
                res.isErrMsg = true;
                res.errMsgNo = 2;
                res.errMsg = "没有查到数据";
            }
            return res;
        }

        //下载数据
        public WUserItemRes GetUserItemRes(WUserItemsReq req)
        {
            WUserItemRes res = new WUserItemRes();
            if (req == null)
            {
                res.isErrMsg = true;
                res.errMsg = "传入参数错误";
                return res;
            }
            res = cbDal.GetUserItemRes(req);
            return res;
        }

        //获取单条抄表记录
        public WUserItemRes GetSingleUserItemRes(WSingleUserItemReq req)
        {
            WUserItemRes res = new WUserItemRes();
            if (req == null)
            {
                res.isErrMsg = true;
                res.errMsg = "传入参数错误";
                return res;
            }
            res = cbDal.GetSingleUserItemRes(req);
            return res;
        }

        public WCBHistoryRes GetUserHistory(WCBHistoryReq req)
        {
            WCBHistoryRes res = new WCBHistoryRes();
            if (req == null)
            {
                res.isErrMsg = true;
                res.errMsg = "传入参数错误";
                return res;
            }
            res = cbDal.GetUserHistory(req);
            return res;
        }

        //获取版本信息
        public VersionInfoRes GetVersionInfo(VersionInfoReq req)
        {
            var verInfo = cbDal.GetVersionInfo(req);
            return verInfo;
        }
        
        /// <summary>
        /// 上传用户数据 RONG 2016-4-7
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public WUploadUserRes UploadUserInfo(WUploadUserReq req)
        {
            var userRes = new WUploadUserRes();
            if (req == null)
            {
                userRes.isErrMsg = true;
                userRes.errMsg = "参数错误";
            }
            else
            {
                userRes = cbDal.UpdataMeterData(req);
            }
            return userRes;
        }
        //上传用户建议
        public WAdviceRes UploadAdvice(WAdviceReq req)
        {
            var adRes = new WAdviceRes();
            if (req == null)
            {
                adRes.isErrMsg = true;
                adRes.errMsg = "参数传递错误";
            }
            else
            {
                adRes = cbDal.AddUserAdvice(req);
            }
            return adRes;
        }

        //获取价格类型
        public WPriceRes GetPriceInfo(WPriceReq req)
        {
            var retRes = new WPriceRes();
            if (req == null)
            {
                retRes.isErrMsg = true;
                retRes.errMsg = "参数错误";
                return retRes;
            }
            retRes = cbDal.GetPriceInfo(req);
            return retRes;
        }
        public WPointRes UploadPoints(WPointReq req)
        {
            var retRes = new WPointRes();
            if (req == null)
            {
                retRes.isErrMsg = true;
                retRes.errMsg = "参数传递错误";
            }
            else
            {
                retRes = cbDal.AddTrack(req);
            }
            return retRes;
        }
        public WFaultReportRes UploadFalut(WFaultReportReq req)
        {
            var res = new WFaultReportRes();
            if (req == null)
            {
                res.isErrMsg = true;
                res.errMsg = "参数传递错误";
                return res;
            }
            res = cbDal.AddFaultReport(req);
            return res;
        }

        public WaterChargeRes AddChargeItem(CharegeReq req)
        {
            var res = new WaterChargeRes();
            if (req == null)
            {
                res.isErrMsg = true;
                res.errMsg = "参数传递错误";
            }
            res = cbDal.AddChargeItem(req);
            return res;
        }

        public PicRes UploadPicData(PicReq req)
        {
            PicRes res = new PicRes();
            res.isErrMsg = false;
            res.errMsgNo = 0;
            var picPath = HttpContext.Current.Server.MapPath("~/temp/"+DateTime.Now.ToString("yyyyMMdd"));
            if (!Directory.Exists(picPath))
            {
                Directory.CreateDirectory(picPath);
            }
            if (req.picData != null)
            {
                string fileName = Path.Combine(picPath, req.picName);
                using (FileStream fs = new FileStream(fileName, FileMode.CreateNew))
                {
                    var bytes = Convert.FromBase64String(req.picData);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                res.errMsgNo = 1;
                res.errMsg = "未接收到相应数据";
            }
            return res;
        }

         public ChargeItemsres GetChargeItemByLoginID(WUserItemsReq req)
        {
              var res= new ChargeItemsres();
              if (req == null)
              {
                  res.isErrMsg = true;
                  res.errMsg = "参数传递错误";
                  return res;
              }
              res = cbDal.GetChargeItemByLoginID(req);
              return res;
         }
        //获取欠费记录
         public QianFeiItemRes getQainFeiItemRes(QianFeiItemReq req)
        {
            var res = new QianFeiItemRes();
            if (req==null)
            {
                 res.isErrMsg = true;
                  res.errMsg = "参数传递错误";
                  return res;
            }
            res = cbDal.getQainFeiItemRes(req);
                 return res;
        }
        //获取服务器时间
        public ServerTimeRes getServerTimes()
         {
             var res = new ServerTimeRes();
             res = cbDal.getServerTimes();
             return res;
         }
        //历史欠费
        public QianFeiHistoryItemRes getQainFeiHistoryItemRes(QianFeiItemReq req)
        {
            var res = new QianFeiHistoryItemRes();
            if (req == null)
            {
                res.isErrMsg = true;
                res.errMsg = "参数传递错误";
                return res;
            }
            res = cbDal.getQainFeiHistoryItemRes(req);
            return res;
        }
        //允许上传时间
        public LoginRes getAllowUpdateDate(WBBReq req)
        {
            var res = new LoginRes();
            if (req==null)
            {
                res.isErrMsg = true;
                res.errMsg = "参数传递错误";
                return res;
            }
            res = cbDal.getAllowUpdateDate(req);
            return res;
        }

        public BaseRes UpdateInvoiceNo(InvoiceReq req)
        {
            return cbDal.UpdateInvoiceInfo(req);
        }

        public WUploadUserRes GetSingleFeeItemRes(WSingleUserItemReq req)
        {
            var res = new WUploadUserRes();
            if (req == null)
            {
                res.isErrMsg = true;
                res.errMsg = "参数传递错误";
                return res;
            }
            res = cbDal.GetSingleFeeItemRes(req);
            return res;
        }

        public MeterPriceRes CalcTotleFee(MeterPriceReq req)
        {
            var res = new MeterPriceRes();
            if (req == null)
            {
                res.isErrMsg = true;
                res.errMsg = "参数错误";
                return res;
            }
            var TrapePrice = cbDal.GetTrapePriceString(req.readMeterRecordId);
            var totleCount = cbDal.getTotleCount(req.waterUserId);
            var arrData = cbDal.GetAllAvePrice(totleCount, req.currData, TrapePrice); //cbDal.GetAvePrice(totleCount, req.currData, TrapePrice);
            if (arrData == null || arrData.Length != 12)
            {
                return res;
            }
            res.calcProc = arrData[1];
            res.TotleFee =toDecimal(arrData[2],0);
            res.avgPrice =toDecimal(arrData[0],0);

            res.step1 = new FeeInfo()
            {
                avgPrice = toDecimal(arrData[3], 0),
                fee = toDecimal(arrData[5], 0),
                waterNum = toDecimal(arrData[4], 0)
            };
            res.step2 = new FeeInfo()
            {
                avgPrice = toDecimal(arrData[6], 0),
                fee = toDecimal(arrData[8], 0),
                waterNum = toDecimal(arrData[7], 0)
            };
            res.step3 = new FeeInfo()
            {
                avgPrice = toDecimal(arrData[9], 0),
                fee = toDecimal(arrData[11], 0),
                waterNum = toDecimal(arrData[10], 0)
            };
            return res;
        }


        public static decimal toDecimal(string str,decimal defaultValue)
        {
            var retVal = 0m;
            if (decimal.TryParse(str, out retVal))
            {
                return retVal;
            }
            return defaultValue;
        }

        public WChargeInfoRes GetChargeInfo(WChargeInfoReq req)
        {
            var res = new WChargeInfoRes();
            if (req == null)
            {
                res.isErrMsg = true;
                res.errMsg = "参数传递错误";
                return res;
            }
            res = cbDal.GetChargeInfoItemRes(req);
            return res;
        }
    }
}