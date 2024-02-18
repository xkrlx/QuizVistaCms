CREATE TABLE answer 
    (
     answer_id INT NOT NULL IDENTITY(1,1), 
     answer_text VARCHAR (100) NOT NULL , 
     is_correct BIT NOT NULL , 
     question_question_id INT NOT NULL 
    )
GO

ALTER TABLE answer ADD CONSTRAINT answer_PK PRIMARY KEY CLUSTERED (answer_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE category 
    (
     category_id INT NOT NULL IDENTITY(1,1), 
     name VARCHAR (40) NOT NULL , 
     description VARCHAR (200) 
    )
GO

ALTER TABLE category ADD CONSTRAINT category_PK PRIMARY KEY CLUSTERED (category_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE question 
    (
     question_id INT NOT NULL IDENTITY(1,1), 
     type CHAR (1) NOT NULL , 
     text VARCHAR (100) NOT NULL , 
     additional_value INT NOT NULL , 
     substractional_value INT , 
     quiz_quiz_id INT NOT NULL , 
     cms_title_style VARCHAR (100) , 
     cms_questions_style VARCHAR (100) 
    )
GO 



EXEC sp_addextendedproperty 'MS_Description' , '1 - one good question
2 - true/false
3 - multi' , 'USER' , 'dbo' , 'TABLE' , 'question' , 'COLUMN' , 'type' 
GO

ALTER TABLE question ADD CONSTRAINT question_PK PRIMARY KEY CLUSTERED (question_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE quiz 
    (
     quiz_id INT NOT NULL IDENTITY(1,1), 
     name VARCHAR (40) NOT NULL , 
     description VARCHAR (200) , 
     creation_date DATETIME NOT NULL , 
     edition_date DATETIME , 
     author VARCHAR , 
     category_category_id INT NOT NULL , 
     cms_title_style VARCHAR (100) 
    )
GO

ALTER TABLE quiz ADD CONSTRAINT quiz_PK PRIMARY KEY CLUSTERED (quiz_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE tags 
    (
     id INT NOT NULL IDENTITY(1,1), 
     name VARCHAR (20) NOT NULL 
    )
GO

ALTER TABLE tags ADD CONSTRAINT tags_PK PRIMARY KEY CLUSTERED (id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

CREATE TABLE tags_quiz 
    (
     tags_id INT NOT NULL , 
     quiz_quiz_id INT NOT NULL 
    )
GO

ALTER TABLE tags_quiz ADD CONSTRAINT tags_quiz_PK PRIMARY KEY CLUSTERED (tags_id, quiz_quiz_id)
     WITH (
     ALLOW_PAGE_LOCKS = ON , 
     ALLOW_ROW_LOCKS = ON )
GO

ALTER TABLE answer 
    ADD CONSTRAINT answer_question_FK FOREIGN KEY 
    ( 
     question_question_id
    ) 
    REFERENCES question 
    ( 
     question_id 
    ) 
    ON DELETE CASCADE 
    ON UPDATE NO ACTION 
GO

ALTER TABLE question 
    ADD CONSTRAINT question_quiz_FK FOREIGN KEY 
    ( 
     quiz_quiz_id
    ) 
    REFERENCES quiz 
    ( 
     quiz_id 
    ) 
    ON DELETE CASCADE 
    ON UPDATE NO ACTION 
GO

ALTER TABLE quiz 
    ADD CONSTRAINT quiz_category_FK FOREIGN KEY 
    ( 
     category_category_id
    ) 
    REFERENCES category 
    ( 
     category_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE tags_quiz 
    ADD CONSTRAINT tags_quiz_quiz_FK FOREIGN KEY 
    ( 
     quiz_quiz_id
    ) 
    REFERENCES quiz 
    ( 
     quiz_id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO

ALTER TABLE tags_quiz 
    ADD CONSTRAINT tags_quiz_tags_FK FOREIGN KEY 
    ( 
     tags_id
    ) 
    REFERENCES tags 
    ( 
     id 
    ) 
    ON DELETE NO ACTION 
    ON UPDATE NO ACTION 
GO


