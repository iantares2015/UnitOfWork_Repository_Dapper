CREATE OR REPLACE FUNCTION get_transactions_by_user_id(user_id INTEGER)
    RETURNS TABLE (
                      CurrentName VARCHAR(50),
                      CurrentBalance REAL,
                      SenderName VARCHAR(50),
                      ReceiverName VARCHAR(50),
                      Amount REAL,
                      TransactionDate TIMESTAMP
                  ) AS $$
BEGIN
    RETURN QUERY
        SELECT
            u.Name as CurrentName,
            u.CurrentBalance,
            CASE
                WHEN t.SourceUserId = $1 THEN (SELECT u2.Name FROM Users u2 WHERE u2.Id = t.TargetUserId)
                ELSE (SELECT u3.Name FROM Users u3 WHERE u3.Id = t.SourceUserId)
                END AS SenderName,
            CASE
                WHEN t.SourceUserId = $1 THEN (SELECT u4.Name FROM Users u4 WHERE u4.Id = t.SourceUserId)
                ELSE (SELECT u5.Name FROM Users u5 WHERE u5.Id = t.TargetUserId)
                END AS ReceiverName,
            t.Amount,
            t.Timestamp
        FROM Transfers t
                 JOIN Users u ON u.Id = $1
        WHERE t.SourceUserId = $1 OR t.TargetUserId = $1;
END;
$$ LANGUAGE plpgsql;
