CREATE FUNCTION update_subscriptions() RETURNS TRIGGER AS $$
	DECLARE
        c_subscription cursor for select * from subscriptions.subscription;
        r_subscription subscriptions.subscription%ROWTYPE;
    BEGIN
        FOR r_subscription IN c_subscription LOOP
            INSERT INTO subscriptions.pendingrequest (subscription_id, request_id) VALUES (r_subscription.id, NEW.id);
        END LOOP;
    END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER tg_subscription_pending_requests AFTER INSERT ON epcis.request FOR EACH ROW EXECUTE PROCEDURE update_subscriptions();