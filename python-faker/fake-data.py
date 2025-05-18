import uuid
import random
from faker import Faker
import psycopg2
import psycopg2.extras

from tqdm import tqdm

fake = Faker()
psycopg2.extras.register_uuid()

# Configurações de conexão
conn = psycopg2.connect(
    host="localhost",
    port=32770,
    dbname="customers_orders_db",
    user="postgres",
    password="postgres"
)

cur = conn.cursor()

def insert_customers(n=10000):
    print(f"[INFO] Inserindo {n} clientes...")
    customers = []
    for i in range(n):
        customer_id = uuid.uuid4()
        name = fake.name()
        email = fake.email()
        customers.append((customer_id, name, email))

        cur.execute(
            f"INSERT INTO customers (customer_id, name, email) VALUES ('{customer_id}', '{name}', '{email}')"
        )
    
        if (i + 1) % 1000 == 0:
            conn.commit()

    print("[INFO] Inserção de clientes finalizada.")
    return [c[0] for c in customers]  # retorna apenas os UUIDs

def insert_orders(customers, orders_per_customer=10000):
    total_orders = len(customers) * orders_per_customer
    print(f"[INFO] Inserindo {total_orders:,} pedidos no total ({orders_per_customer} por cliente)...")

    for idx, customer_id in enumerate(customers):
        orders = []
        for _ in range(orders_per_customer):
            order_id = uuid.uuid4()
            product_name = fake.word()
            quantity = random.randint(1, 10)
            total_value = round(quantity * random.uniform(10.0, 500.0), 2)
            orders.append((order_id, customer_id, product_name, quantity, total_value))

        cur.execute(
            f"INSERT INTO orders (order_id, customer_id, product_name, quantity, total_value) VALUES ('{order_id}', '{customer_id}', '{product_name}', {quantity}, {total_value})"
        )


        if (idx + 1) % 1000 == 0:
            conn.commit()
            print(f"[INFO] {idx + 1}/{len(customers)} clientes processados.")

    print("[INFO] Inserção de pedidos finalizada.")

try:
    customers = insert_customers()
    insert_orders(customers)
finally:
    cur.close()
    conn.close()
    print("[INFO] Conexão encerrada.")
