#!/usr/bin/python

# Import the CGI, string, sys modules
import cgi, string, sys, os, re, random
import cgitb; cgitb.enable()  # for troubleshooting
import sqlite3

#Get Constants
DATABASE="/homes/bsporter/apache/htdocs/Sql/database_access/database/FlappyGustavo.db"

###################################################################
def get_top_scores():
    conn = sqlite3.connect(DATABASE)
    c = conn.cursor()

    sql = """
    	SELECT 
	    user_name,
	    score
	FROM users
	ORDER BY score DESC
	LIMIT 10
    """
    
    c.execute('SELECT user_name, score FROM users ORDER BY score DESC LIMIT 10')
	
    row = c.fetchall()
	
    if row == None:
      return None
	
    conn.close()

    return row
    
####################################################################
def add_score(userName, score):
    conn = sqlite3.connect(DATABASE)
    c = conn.cursor()

    t = (None, userName, score)
    c.execute('INSERT INTO users(user_id, user_name, score) VALUES(?,?,?)', t)

    conn.commit()
    conn.close()

    return

####################################################################
def check_delete_scores():
    conn = sqlite3.connect(DATABASE)
    c = conn.cursor()

    c.execute('SELECT COUNT(*) FROM users')

    row = c.fetchone()

    if row == None:
      conn.close()
      return

    count = row[0]

    if count > 100:
      # Delete scores
      sql = """
      	DELETE FROM users WHERE

      """

      c.execute(sql)

      conn.commit()

    conn.close()

    return

####################################################################
def clear_scores():
    conn = sqlite3.connect(DATABASE)
    c = conn.cursor()

    c.execute('DELETE FROM users')

    conn.commit()
    conn.close()

    return
