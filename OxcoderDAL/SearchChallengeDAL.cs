﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// 搜索 的摘要说明
/// 此类是 DAL 的实现类
/// </summary>
/// 
/// Author:岚岚姐
/// Date：2015/06/09
///
namespace OxcoderDAL
{
    public class SearchChallengeDAL : OxcoderIDAL.SearchChallengeIDAL
    {
        public DataSet SearchUseCondition(int salary, string provice, int retype, int flag, string searchCondition, int pageindex, int pagesize)
        {
            StringBuilder sql = new StringBuilder();
            List<SqlParameter> par = new List<SqlParameter>();
            sql.Append("select * from [Challenge] as c,[Enterprice] as e where c.Challenge_OwnerID = e.Enterprice_ID ");
            if (salary != -1 && salary != 0)
            {
                sql.Append(" and Challenge_Salary = @salary");
                SqlParameter mParam = new SqlParameter("@salary", SqlDbType.Int);
                mParam.Value = salary;
                par.Add(mParam);
            }
            if (provice != null)
            {
                sql.Append(" and Challenge_Area like @provice");
                SqlParameter mParam = new SqlParameter("@provice", SqlDbType.Text);
                mParam.Value = provice;
                par.Add(mParam);
            }
            if (searchCondition != null)
            {
                sql.Append(" and Challenge_Name like @searchCondition");
                SqlParameter mParam = new SqlParameter("@searchCondition", SqlDbType.Text);
                mParam.Value = searchCondition;
                par.Add(mParam);
            }
            if (retype != -1 && retype != 0)
            {
                sql.Append(" and Challenge_Type = @retype");
                SqlParameter mParam = new SqlParameter("@retype", SqlDbType.Int);
                mParam.Value = retype;
                par.Add(mParam);
            }
            if (flag == -1 || flag == 3)
            {
                sql.Append(" order by Challenge_Publish");
            }
            else if (flag == 2)
            {
                sql.Append(" order by Challenge_Salary");
            }
            else if (flag == 1)
            {
                sql.Append(" order by Challenge_Num");
            }
            return Common.DbHelperSQL.PageQuery(sql.ToString(), pageindex, pagesize, par.ToArray());
        }

        public DataSet SearchByUserHistory(string userid, int state, int pageindex, int pagesize)
        {
            StringBuilder sql = new StringBuilder();
            List<SqlParameter> par = new List<SqlParameter>();
            if (state == -1)
            {
                sql.Append("select * from [Challenge] as c,[Enterprice] e where c.Challenge_OwnerID = e.Enterprice_ID and Challenge_State=0 and Challenge_ID in (select Test_ChallengeID from Test where Test_UserID like @userid)");
                SqlParameter mParam = new SqlParameter("@userid", SqlDbType.Text);
                mParam.Value = userid;
                par.Add(mParam);
            }
            else
            {
                sql.Append("select * from [Challenge] as c,[Enterprice] e where c.Challenge_OwnerID = e.Enterprice_ID and Challenge_State=0 and Challenge_ID in (select Test_ChallengeID from Test where Test_UserID like @userid and Test_State = @state)");
                SqlParameter mParam = new SqlParameter("@userid", SqlDbType.Text);
                mParam.Value = userid;
                par.Add(mParam);
                mParam = new SqlParameter("@state", SqlDbType.Int);
                mParam.Value = state;
                par.Add(mParam);
            }
            return Common.DbHelperSQL.PageQuery(sql.ToString(), pageindex, pagesize, par.ToArray());
        }

        public DataSet SearchByUser(string userid, int state, int pageindex, int pagesize)
        {
            StringBuilder sql = new StringBuilder();
            List<SqlParameter> par = new List<SqlParameter>();
            if (state == -1)
            {
                sql.Append("select * from [Challenge] as c,[Enterprice] e where c.Challenge_OwnerID = e.Enterprice_ID and Challenge_State=1 and Challenge_ID in (select Test_ChallengeID from Test where Test_UserID like @userid)");
                SqlParameter mParam = new SqlParameter("@userid", SqlDbType.Text);
                mParam.Value = userid;
                par.Add(mParam);
            }
            else
            {
                sql.Append("select * from [Challenge] as c,[Enterprice] e where c.Challenge_OwnerID = e.Enterprice_ID and Challenge_State=1 and Challenge_ID in (select Test_ChallengeID from Test where Test_UserID like @userid and Test_State = @state)");
                SqlParameter mParam = new SqlParameter("@userid", SqlDbType.Text);
                mParam.Value = userid;
                par.Add(mParam);
                mParam = new SqlParameter("@state", SqlDbType.Int);
                mParam.Value = state;
                par.Add(mParam);
            }
            return Common.DbHelperSQL.PageQuery(sql.ToString(), pageindex, pagesize, par.ToArray());
        }

        public DataSet SearchByOwner(string id, int pageindex, int pagesize)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from [Challenge] as c,[Enterprice] as e where c.Challenge_OwnerID = e.Enterprice_ID and e.Enterprice_ID like @id ");
            SqlParameter[] par = { new SqlParameter("@id", SqlDbType.Text) };
            par[0].Value = id;
            return Common.DbHelperSQL.PageQuery(sql.ToString(), pageindex, pagesize, par.ToArray());
        }

        public DataSet SearchByChallengeID(string id, int pageindex, int pagesize)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from Challenge where Challenge_OwnerID like @id");
            SqlParameter[] par ={new SqlParameter("@id",SqlDbType.Text)};
            par[0].Value = id;
            return Common.DbHelperSQL.PageQuery(sql.ToString(), pageindex, pagesize, par.ToArray());
        }
    }
}