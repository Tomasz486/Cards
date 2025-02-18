
CREATE DATABASE
[Cards]
ON PRIMARY (
    NAME=Test_data,
    FILENAME = '{0}\\Cards.mdf'
)
LOG ON (
    NAME=Test_log,
    FILENAME = '{0}\\Cards.ldf'
)