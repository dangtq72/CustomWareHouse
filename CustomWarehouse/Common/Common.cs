using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CW_Info;
using CW_Business;

namespace CustomWarehouse
{
    public class Common
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static int cNumberRecordOnPage = 20;
    }

    public class CommonData
    {
        public static char CharSplit = '|';

        /// <summary> 
        /// </summary>
        public static List<Groups_Info> gLstGroups = new List<Groups_Info>();

        //hma get 
        public static List<Groups_Info> GetLstGroups()
        {
            try
            {
                GroupsBL _groups = new GroupsBL();
                gLstGroups = _groups.GroupsGetAll();
                return gLstGroups;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Groups_Info>();
            }
        }

        public static List<User_Info> glstUserOfGroupAll = new List<User_Info>();

        public static List<User_Info> GetlstUserOfGroupAll()
        {
            try
            {

                GroupUserBL _groups = new GroupUserBL();
                glstUserOfGroupAll = _groups.GetUserOfGroupByIDGroup(0);

                return glstUserOfGroupAll;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<User_Info>();
            }
        }

        /// <summary>
        /// Lưu danh sách các chức năng trên hệ thống 
        /// phục vụ tìm kiếm(search master )
        /// </summary>
        public static List<Functions_Info> gLstFunction = new List<Functions_Info>();

    }
}