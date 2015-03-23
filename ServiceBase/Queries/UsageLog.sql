SELECT * FROM UsageLog
Join NetworkLogin On NetworkLogin.id = UsageLog.NTID_id;