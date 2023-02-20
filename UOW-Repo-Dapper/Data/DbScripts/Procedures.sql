CREATE OR REPLACE PROCEDURE create_product(
    IN title TEXT,
    IN price REAL
)
    LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO Products (Title, Price)
    VALUES (title, price);
END;
$$;
