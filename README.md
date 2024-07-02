# Takeaway
微服务项目

## Takeaway.Services.CouponAPI

折扣API

数据存储：postgres

> ​	docker pull postgres
>
> ​	docker pull dpage/pgadmin4   用于连接postgres的管理工具
>
> ​	docker run -d -p 5432:5432 --name postgresql -v pgdata:/var/lib/postgresql/data -e POSTGRES_PASSWORD=admin123 postgres
>
> docker run -d -p 5433:80 --name pgadmin4 -e PGADMIN_DEFAULT_EMAIL=test@123.com -e PGADMIN_DEFAULT_PASSWORD=123456 dpage/pgadmin4

ORM：Dapper

表结构：Coupon

| 字段名称       | 字段类型    | 字段描述 |
| -------------- | ----------- | -------- |
| couponid       | int         | 主键     |
| couponcode     | varchar(24) | 优惠码   |
| discountamount | double      | 优惠金额 |
| minamount      | double      | 最低金额 |

## Takeaway.Services.AuthAPI

身份认证API

数据存储：mysql

> ​	docker pull mysql:5.7
>
> ​	mkdir ~/mysql 
>
> ​	cd ~/mysql
>
> ​	docker run -d -p 3306:3306 --name c_mysql -v $PWD/conf:/etc/mysql/conf.d -v $PWD/logs:/logs -v $PWD/data:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456 mysql:5.7

docker run -d -p 3306:3306 --name auth_mysql -v $PWD/conf:/etc/mysql/conf.d -v $PWD/logs:/logs -v $PWD/data:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456 mysql:5.7

ORM：EFCore

## Takeaway.Services.ProductAPI

产品API

数据存储：mongodb

> ​	docker pull mongo
>
> ​	 docker run --name product-mongo -d -p 27017:27017 -v /home/mongo/mymongo:/data/db  mongo

## Takeaway.Services.ShoppingCartAPI

购物车API

数据存储：Redis

> 1.docker pull redis:7.0.5
>
>  2.mkdir ~/redis 
>
>  3.cd ~/redis
>
> docker run -d -p 6379:6379 --name=redis --privileged=true -v $PWD/conf/redis.conf:/etc/redis/redis.conf -v $PWD/data:/data redis:7.0.5 redis-server /etc/redis/redis.conf --appendonly yes

redis.conf

```sh
bind 0.0.0.0
protected-mode yes
port 6379
tcp-backlog 511
timeout 0
tcp-keepalive 300
daemonize no
pidfile /var/run/redis_6379.pid
loglevel notice
logfile ""
databases 16
always-show-logo no
set-proc-title yes
proc-title-template "{title} {listen-addr} {server-mode}"
stop-writes-on-bgsave-error yes
rdbcompression yes
rdbchecksum yes
dbfilename dump.rdb
rdb-del-sync-files no
dir /data
requirepass 123456
replica-serve-stale-data yes
replica-read-only yes
repl-diskless-sync no
repl-diskless-sync-delay 5
repl-diskless-load disabled
repl-disable-tcp-nodelay no
replica-priority 100
acllog-max-len 128
lazyfree-lazy-eviction no
lazyfree-lazy-expire no
lazyfree-lazy-server-del no
replica-lazy-flush no
lazyfree-lazy-user-del no
lazyfree-lazy-user-flush no
oom-score-adj no
oom-score-adj-values 0 200 800
disable-thp yes
appendonly yes
appendfilename "appendonly.aof"
appendfsync everysec
no-appendfsync-on-rewrite no
auto-aof-rewrite-percentage 100
auto-aof-rewrite-min-size 64mb
aof-load-truncated yes
aof-use-rdb-preamble yes
lua-time-limit 5000
slowlog-log-slower-than 10000
slowlog-max-len 128
latency-monitor-threshold 0
notify-keyspace-events ""
hash-max-ziplist-entries 512
hash-max-ziplist-value 64
list-max-ziplist-size -2
list-compress-depth 0
set-max-intset-entries 512
zset-max-ziplist-entries 128
zset-max-ziplist-value 64
hll-sparse-max-bytes 3000
stream-node-max-bytes 4096
stream-node-max-entries 100
activerehashing yes
client-output-buffer-limit normal 0 0 0
client-output-buffer-limit replica 256mb 64mb 60
client-output-buffer-limit pubsub 32mb 8mb 60
hz 10
dynamic-hz yes
aof-rewrite-incremental-fsync yes
rdb-save-incremental-fsync yes
jemalloc-bg-thread yes
```

