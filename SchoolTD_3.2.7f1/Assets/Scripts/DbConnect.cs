using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class DbConnect : MonoBehaviour
{
    private MySqlConnection con;
    public DbConnect(string host, string dbname, string ui, string pw)
    {
        con = new MySqlConnection($"Database = {dbname}; Data Source = {host}; User Id = {ui}; Password = {pw};");
    }
    private bool Connect()
    {
        try
        {
            con.Open();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    private bool Connect_close()
    {
        try
        {
            con.Close();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public void InsertPlayer(player temp)
    {
        if (Connect())
        {
            string query = "INSERT INTO player(username,pw) VALUES(@username,@pw);";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", temp.Username);
            cmd.Parameters.AddWithValue("@pw", temp.Pw);
            cmd.ExecuteNonQuery();
            Connect_close();
        }
    }
    public List<player> CountUserPlayer(player tempUser)
    {
        //dunno?
        List<player> temp = new();
        if (Connect())
        {
            string query = "select Count(*) as db from player where username like @username;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", tempUser.Username);
            MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    temp.Add(new player(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2)
                        ));
                }
            Connect_close();
        }
        return temp;
    }
    public List<player> SelectAllPlayer()
    {
        List<player> temp = new();
        if (Connect())
        {
            string query = "select * from player;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    temp.Add(new player(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2)
                        ));
                }
            Connect_close();
        }
        return temp;
    }
}
