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
    public List<waves> SelectWave(int id) {
        List<waves> temp = new();
        if (Connect())
        {
            string query = "SELECT * FROM waves WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id",id);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.Add(new(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3)
                    ));
            }
            Connect_close();
        }
        return temp;
    }

    public List<effects> SelectEffects() {
        List<effects> temp = new();
        if (Connect())
        {
            string query = "SELECT * FROM effects";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.Add(new(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2)
                    ));
            }
            Connect_close();
        }
        return temp;
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
            string query = "SELECT Count(username) as db FROM player WHERE username LIKE @username;";
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
    public void InsertPlayerTower(playerTower temp){
        if(Connect()){

            string query = "INSERT INTO playertower(towerID,playerID,currentXP) VALUES(@towerID,@playerID,@EXP);";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@towerID", temp.TowerID);
            cmd.Parameters.AddWithValue("@playerID", temp.PlayerID);
            cmd.Parameters.AddWithValue("@currentXP", temp.CurrentXP);
            cmd.ExecuteNonQuery();
            Connect_close();
        }
    }
    public void InsertTower(TotalTower temp) {
        if (Connect())
        {

            string query = "INSERT INTO totaltower(towerMaxLVL,LVLup,currentLVL,effectID) VALUES(@towerMaxLVL,@LVLup,@currentLVL,@effectID);";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@towerMaxLVL", temp.TowerMaxLVL);
            cmd.Parameters.AddWithValue("@LVLup", temp.LvlUP);
            cmd.Parameters.AddWithValue("@currentLVL", temp.CurrentLVL);
            cmd.Parameters.AddWithValue("@effectID", temp.EffectID);
            cmd.ExecuteNonQuery();
            Connect_close();
        }
    }
    public List<playerTower> SelectPlayerTower(int playerid) {
        List<playerTower> temp = new();
        if (Connect())
        {
            string query = "SELECT * FROM playertower WHERE playerID = @playerID;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@playerID", playerid);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.Add(new(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3)
                    ));
            }
            Connect_close();
        }
            return temp;
    }

    public List<TotalTower> SelectTower(int towerid)
    {
        List<TotalTower> temp = new();
        if (Connect())
        {
            string query = "SELECT * FROM totaltower WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", towerid);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.Add(new(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3),
                    reader.GetInt32(4)
                    ));
            }
            Connect_close();
        }
        return temp;
    }
    public void InsertOrigami(origami temp) {
        if (Connect())
        {

            string query = "INSERT INTO origami(baseHp,baseSpeed,baseDef,effectID,Exp) VALUES(@baseHp,@baseSpeed,@baseDef,@effectID,@Exp);";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@baseHp", temp.BaseHP);
            cmd.Parameters.AddWithValue("@baseSpeed", temp.BaseSpeed);
            cmd.Parameters.AddWithValue("@baseDef", temp.BaseDef);
            cmd.Parameters.AddWithValue("@effectID", temp.EffectID);
            cmd.Parameters.AddWithValue("@Exp", temp.EXP);
            cmd.ExecuteNonQuery();
            Connect_close();
        }
    }
    public List<origami> SelectOrigami(int origamiid) {
        List<origami> temp = new();
        if (Connect())
        {
            string query = "SELECT * FROM origami WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", origamiid);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.Add(new(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3),
                    reader.GetInt32(4),
                    reader.GetInt32(5)
                    ));
            }
            Connect_close();
        }
        return temp;
    }
    public List<levels> SelectLevel(int levelid) {
        List<levels> temp = new();
        if (Connect()) {
            string query = "SELECT * FROM levels WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", levelid);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.Add(new(
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3),
                    reader.GetInt32(4)
                    ));
            }
            Connect_close();
        }
        return temp;
    }
}
