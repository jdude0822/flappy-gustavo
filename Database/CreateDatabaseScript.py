#!/usr/bin/python

import sqlite3
conn = sqlite3.connect('FlappyGustavo.db')

c = conn.cursor()

# Turn on foreign key support
c.execute("PRAGMA foreign_keys = ON")

# Create users table
# -- Stores all users
c.execute('''CREATE TABLE users
	     (user_id INTEGER NOT NULL,
	      user_name TEXT NOT NULL, 
	      score INTEGER NOT NULL,
	      PRIMARY KEY (user_id))''')

# Save the changes
conn.commit()

# Close the connection
conn.close()
