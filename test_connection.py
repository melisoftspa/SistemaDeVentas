import pymssql

try:
    conn = pymssql.connect(host='localhost', database='master')
    print("Conexión exitosa")
    conn.close()
except Exception as e:
    print(f"Error: {e}")