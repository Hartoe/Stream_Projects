import sqlite3
import cmds

connection = sqlite3.connect('database.db')

def init_database():
    c = connection.cursor()
    c.execute('CREATE TABLE IF NOT EXISTS commands (name text PRIMARY KEY, message text)')
    c.execute('CREATE TABLE IF NOT EXISTS quotes (quote text PRIMARY KEY, date DATETIME)')
    connection.commit()

def insert_command(cmd: cmds.Cmd):
    c = connection.cursor()
    c.execute('INSERT INTO commands VALUES (?, ?)', (cmd.name, cmd.message))
    connection.commit()

def update_command(cmd: cmds.Cmd):
    c = connection.cursor()
    c.execute('UPDATE commands SET message = ? WHERE name = ?', (cmd.message, cmd.name))
    connection.commit()

def delete_command(cmd: cmds.Cmd):
    c = connection.cursor()
    c.execute('DELETE FROM commands WHERE name = ?', (cmd.name,))
    connection.commit()

def select_commands():
    c = connection.cursor()
    c.execute('SELECT name, message FROM commands')
    cs = []
    for name, message in c.fetchall():
        cs.append(cmds.TextCmd(name, message))
    connection.commit()
    return cs

def insert_quote(quote, date):
    c = connection.cursor()
    c.execute('INSERT INTO quotes (quote, date) VALUES (?, ?)', (quote, date))
    connection.commit()

def delete_quote(quote):
    c = connection.cursor()
    c.execute('DELETE FROM quotes WHERE quote = ?', (quote,))
    connection.commit()

def select_quote():
    c = connection.cursor()
    c.execute('SELECT quote, date FROM quotes ORDER BY RANDOM() LIMIT 1')
    connection.commit()
    return c.fetchone()