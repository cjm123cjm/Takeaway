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
| minamount      | int         | 最低数量 |

## Takeaway.Services.AuthAPI

身份认证API

数据存储：sqlserver

ORM：EFCore

## Takeaway.Services.ProductAPI

产品API

数据存储：mongodb

> ​	docker pull mongo
>
> ​	 docker run --name product-mongo -d -p 27017:27017  mongo
