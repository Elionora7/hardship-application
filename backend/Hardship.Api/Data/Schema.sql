CREATE TABLE IF NOT EXISTS hardship_applications (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    customer_name TEXT NOT NULL,
    date_of_birth TEXT NOT NULL,
    income DECIMAL(18,2) NOT NULL,
    expenses DECIMAL(18,2) NOT NULL,
    hardship_reason TEXT,
    created_at TEXT NOT NULL,
    updated_at TEXT NOT NULL
);
