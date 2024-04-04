using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

public class DbConnect
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
    public int CountUserPlayer(string tempUser)
    {
        //dunno?
        int count = 0;
        if (Connect())
        {
            string query = "select Count(username) as db from player where username like @username;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", tempUser);
            MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count += reader.GetInt32(0);
                }
            Connect_close();
        }
        return count;
    }
    public List<player> SelectAllPlayer()
    {
        List<player> temp = new();
        if (Connect())
        {
            string query = "SELECT * FROM player;";
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
    public void InsertHighscore(highscores temp) {
        if (Connect())
        {
            string query = "INSERT INTO highscores(playerId,score) VALUES(@playerId,@score);";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@playerId", temp.PlayerId);
            cmd.Parameters.AddWithValue("@score", temp.Score);
            cmd.ExecuteNonQuery();
            Connect_close();
        }
    }
    public void LevelCleared(clearedLevel temp)
    {
        if (Connect())
        {
            string query = "INSERT INTO clearlevels(playerId,lvlId,dif,score) VALUES(@playerId,@lvlId,@dif,@score);";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@playerId", temp.PlayerID);
            cmd.Parameters.AddWithValue("@lvlId", temp.LevelID);
            cmd.Parameters.AddWithValue("@dif", temp.Dif);
            cmd.Parameters.AddWithValue("@score", temp.Score);
            cmd.ExecuteNonQuery();
            Connect_close();
        }
    }
}
