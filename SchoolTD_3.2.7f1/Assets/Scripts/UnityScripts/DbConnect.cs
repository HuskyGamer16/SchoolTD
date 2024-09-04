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
            string query = "INSERT INTO player(username,password) VALUES(@username,@password);";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", temp.Username);
            cmd.Parameters.AddWithValue("@password", temp.Pw);
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
                    reader.GetString(2),
                    reader.GetInt32(3),
                    reader.GetInt32(4)
                    ));
            }
            Connect_close();
        }
        return temp;
    }
    public void TowerXPgain(int playerid, int xp) {
        if (Connect()) {
            string query = "Update player set Xp = xp + @xp where id = @id";
            MySqlCommand cmd = new(query, con);
            cmd.Parameters.AddWithValue("@id",playerid);
            cmd.Parameters.AddWithValue("@xp",xp);
            cmd.ExecuteNonQuery();
            Connect_close ();
        }
    }

    public void LevelCleared(int playerid,int lvlscore)
    {
        if (Connect())
        {
            //string query = "UPDATE playertower SET towerid = towerid + 1, currentXP = 0 WHERE towerID = @towerid AND playerID = @playerid";
            string query = "Update player set score = score + @lvlscore where id = @Id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", playerid);
            cmd.Parameters.AddWithValue("@lvlscore", lvlscore);
            cmd.ExecuteNonQuery();
            Connect_close();
        }
    }
    public void InsertPlayerTower(playerTower temp){
        if(Connect()){
            string query = "INSERT INTO ptowers(playerID,towerID,XP) VALUES(@playerID,@towerID,@EXP);";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@towerID", temp.TowerID);
            cmd.Parameters.AddWithValue("@playerID", temp.PlayerID);
            cmd.Parameters.AddWithValue("@EXP", temp.Exp);
            cmd.ExecuteNonQuery();
            Connect_close();
        }
    }
    public void InsertTower(TotalTower temp) {
        if (Connect())
        {
            string query = "INSERT INTO towers(name,dmg,LVL,XP) VALUES(@name,@dmg,@lvl,@xp);";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", temp.Name);
            cmd.Parameters.AddWithValue("@dmg", temp.Dmg);
            cmd.Parameters.AddWithValue("@lvl", temp.Lvl);
            cmd.Parameters.AddWithValue("@xp", temp.Exp);
            cmd.ExecuteNonQuery();
            Connect_close();
        }
    }
    public List<playerTower> SelectPlayerTower(int playerid) {
        List<playerTower> temp = new();
        if (Connect())
        {
            string query = "SELECT * FROM ptowers WHERE playerID = @playerID order by towerID desc;";
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
    public int GetMaxLvl(int towerid) {
        int max = 0;
        string temp = "";
        if (Connect()) {
            string query = "Select name from towers where id = @towerid;";
            MySqlCommand cmd = new MySqlCommand(query,con);
            cmd.Parameters.AddWithValue("@towerid",towerid);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) {
                temp = reader.GetString(0);
            }
            Connect_close();
            if (Connect()) {
                query = "Select Lvl from towers where name like @name order by lvl desc limit 1;";
                cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", temp);
                reader = cmd.ExecuteReader();
                while (reader.Read()) { 
                max = reader.GetInt32(0);
                }
                Connect_close();
            }
        }
        return max;
    }
    public float GetReqXp(int towerid, int playerid) {
        int lvl = 0;
        if (Connect())
        {
            string query = "Select towers.xp, ptowers.xp from towers inner join ptowers (towers.id = ptowers.towerid) where idkblahblahblah";
            MySqlCommand cmd = new MySqlCommand(query, con);

            Connect_close();
        }
        return lvl;
    }
    public float GetLvlXp(int towerid, int playerid) {
        int lvl = 0;
        if (Connect())
        {
            
            Connect_close();
        }
        return lvl;
    }
    public int GetLvl(int towerid)
    {
        int lvl = 0;
        if (Connect())
        {
            string query = "Select lvl from towers where id = @id;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", towerid);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lvl = reader.GetInt32(0);
            }
            Connect_close();
        }
        return lvl;
    }
    public List<playerTower> SelectPlayerTower(int playerid, string towername)
    {
        List<playerTower> temp = new();
        if (Connect())
        {
            string query = "SELECT * FROM ptowers left join towers on (ptowers.towerid = towers.id) WHERE ptowers.playerID = @playerID and towers.name=@TowerName, order by towerID desc;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@playerID", playerid);
            cmd.Parameters.AddWithValue("@TowerName", towername);
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
            string query = "SELECT * FROM towers WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", towerid);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.Add(new(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetInt32(2),
                    reader.GetInt32(3),
                    reader.GetInt32(4)
                    ));
            }
            Connect_close();
        }
        return temp;
    }
   
    public List<TotalTower> SelectAllTower()
    {
        List<TotalTower> temp = new();
        if (Connect())
        {
            string query = "SELECT * FROM towers;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.Add(new(
                    reader.GetInt32(0),
                    reader.GetString(1),
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
            string query = "INSERT INTO origami(baseHp,baseSpeed,baseDef,Exp) VALUES(@baseHp,@baseSpeed,@baseDef,@Exp);";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@baseHp", temp.BaseHP);
            cmd.Parameters.AddWithValue("@baseSpeed", temp.BaseSpeed);
            cmd.Parameters.AddWithValue("@baseDef", temp.BaseDef);
            cmd.Parameters.AddWithValue("@Exp", temp.EXP);
            cmd.ExecuteNonQuery();
            Connect_close();
        }
    }
    public int GetLevels(int playerid) {
        int ret = 0;
        if (Connect())
        {
            string query = "SELECT crntlevel FROM player WHERE id = @id;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id",playerid);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ret = reader.GetInt32(0);
            }
            Connect_close();
        }
        return ret;
    }
    public List<origami> SelectOrigami() {
        List<origami> temp = new();
        if (Connect())
        {
            string query = "SELECT * FROM origami;";
            MySqlCommand cmd = new MySqlCommand(query, con);
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
    public void TowerLvlUP(int towerid,int playerid) {
        if (Connect()) {
            string query = "";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            Connect_close();
        }
    }
}
