use poc_crm;
-- Companies
INSERT INTO company (name, domain_name) VALUES
  ('TechNova', 'technova.com'),
  ('GreenFoods', 'greenfoods.org'),
  ('Skyline Media', 'skylinemedia.net'),
  ('HealthFirst', 'healthfirst.io'),
  ('EduSmart', 'edusmart.edu');

-- Contacts para TechNova
INSERT INTO contact (first_name, last_name, email, phone, company_name) VALUES
  ('Alice', 'Johnson', 'alice.johnson@technova.com', '+55 11 91234-0001', 'TechNova'),
  ('Bruno', 'Silva', 'bruno.silva@technova.com', '+55 11 91234-0002', 'TechNova');

-- Contacts para GreenFoods
INSERT INTO contact (first_name, last_name, email, phone, company_name) VALUES
  ('Carla', 'Ferreira', 'carla.ferreira@greenfoods.org', '+55 21 92345-1001', 'GreenFoods'),
  ('Diego', 'Almeida', 'diego.almeida@greenfoods.org', '+55 21 92345-1002', 'GreenFoods');

-- Contacts para Skyline Media
INSERT INTO contact (first_name, last_name, email, phone, company_name) VALUES
  ('Elena', 'Costa', 'elena.costa@skylinemedia.net', '+55 31 93456-2001', 'Skyline Media'),
  ('Fabio', 'Pereira', 'fabio.pereira@skylinemedia.net', '+55 31 93456-2002', 'Skyline Media');

-- Contacts para HealthFirst
INSERT INTO contact (first_name, last_name, email, phone, company_name) VALUES
  ('Gabriela', 'Souza', 'gabriela.souza@healthfirst.io', '+55 41 94567-3001', 'HealthFirst'),
  ('Henrique', 'Melo', 'henrique.melo@healthfirst.io', '+55 41 94567-3002', 'HealthFirst');

-- Contacts para EduSmart
INSERT INTO contact (first_name, last_name, email, phone, company_name) VALUES
  ('Isabela', 'Martins', 'isabela.martins@edusmart.edu', '+55 51 95678-4001', 'EduSmart'),
  ('João', 'Oliveira', 'joao.oliveira@edusmart.edu', '+55 51 95678-4002', 'EduSmart');
