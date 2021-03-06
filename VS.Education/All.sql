***************************
// tìm toàn bộ hoa hồng của bảng hoa hồng với gián tiếp =1
SELECT (
SELECT SUM(CONVERT(float,innerTable.SoCoin)) 
FROM
(SELECT TOP 5 HoaHongThanhVien.SoCoin FROM HoaHongThanhVien 
where
HoaHongThanhVien.IDUserNguoiDuocHuong=users.iuser_id and HoaHongThanhVien.IDType in (1)
and HoaHongThanhVien.SoCoin is not null) as innerTable) as [TONGCOINHH] , vuserun 
FROM users where (SELECT SUM(CONVERT(float,innerTable.SoCoin)) 
FROM (SELECT TOP 5 HoaHongThanhVien.SoCoin FROM HoaHongThanhVien 
where
HoaHongThanhVien.IDUserNguoiDuocHuong=users.iuser_id and HoaHongThanhVien.IDType in (1) 
and HoaHongThanhVien.SoCoin is not null) as innerTable  ) >0

***************************
// thống kê xem mình đã giới thiệu ra bao nhiêu cấp dưới
select iuser_id as Thanhvien ,
 (SELECT COUNT(*) FROM users as con WHERE((MTree LIKE N'%|'+CONVERT(varchar,cha.iuser_id)+'|%'))) 
from users as cha where cha.iuser_id=15



select  top 10 iuser_id ,vuserun,vaddress,Type,IDChiNhanh,Leader,
 (SELECT COUNT(*) FROM users  WHERE((MTree LIKE N'%|'+CONVERT(varchar,cha.iuser_id)+'|%'))) 
from users as cha where cha.iuser_id=28


 
select iuser_id ,vuserun,vaddress,Type,(SELECT Name FROM menu  WHERE ID=cha.IDChiNhanh) as ChiNhanhs,Leader,
 (SELECT COUNT(*) FROM users as con WHERE((con.MTree LIKE N'%|'+CONVERT(varchar,cha.iuser_id)+'|%')))  as aaaaaa
from users as cha where  cha.GioiThieu=28



***************************

Tìm toàn bộ cấp dưới của mình theo các thôn tin 
sELECT iuser_id,vuserun,vphone,vemail,vaddress,MTree FROM users  WHERE((MTree LIKE N'%|'+CONVERT(varchar,1195)+'|%'))




*********************


SELECT (select vuserun from users where iuser_id=[ChuyenDiemThanhVien].IDNguoiNhan) as nguoinhan,(select vfname from users where iuser_id=[ChuyenDiemThanhVien].IDNguoiNhan) as nguoinhan,* FROM [aggroupusa].[dbo].[ChuyenDiemThanhVien] where IDNguoiCap=15


select (select sum( CONVERT(float, HoaHongThanhVien.SoCoin))as socois from HoaHongThanhVien where HoaHongThanhVien.IDUserNguoiDuocHuong=users.iuser_id and HoaHongThanhVien.IDType in (1)) as tonghoahong from users

SELECT sum(convert(float,(SoDiemCoin))) FROM [aggroupusa].[dbo].CapDiemThanhVien where IDNguoiNhanDiemCoin=81

SELECT sum(convert(float,(Socoin))) FROM HoaHongThanhVien where IDNguoiCap=15

select * from users where iuser_id=81
select * from users where iuser_id in and IDType in (1,2,3,5)
1,2,3,5


SELECT  sum(convert(float,(Socoin))) as sodiem from HoaHongThanhVien where IDUserNguoiDuocHuong=81 
and IDType in (2)

Group by IDUserNguoiDuocHuong
order by sodiem desc


 select * from HoaHongThanhVien
select * from users  where CONVERT(varchar,MTree) is null

select * from users where iuser_id=81

select * from users where GioiThieu=83
select * from users where GioiThieu in (82, 83, 86, 92, 93, 96)

select * from users where GioiThieu in (85,91,94,99,100,104,105,107,109,110,111,112,113,114,118,120,122,123,126,131,135,147,176,186,204,269,532,600,737,835,915,922,976,978,1088,1090,1134,1170,1219,2837,2978,4611,4630,11059,12127,19217)

select  count(valu)  as TongCong from dbo.udfNodeChirent(81)
select  count(valu)  as TongCong from dbo.udfNodeChirent(83)

select * from users where GioiThieu in (85,91,94,99,100,104,105,107,109,110,111,112,113,114,118,120,122,123,126,131,135,147,176,186,204,269,532,600,737,835,915,922,976,978,1088,1090,1134,1170,1219,2837,2978,4611,4630,11059,12127,19217)

SELECT * from HoaHongThanhVien where IDUserNguoiDuocHuong=5763  and IDType in (1)
select * from users where GioiThieu=5763

SELECT  sum(convert(float,(Socoin))) as sodiem from HoaHongThanhVien where IDUserNguoiDuocHuong=81 
and IDType in (1)

Group by IDUserNguoiDuocHuong
order by sodiem desc




*****************


delete from HoaHongThanhVien
delete from LichSuGiaoDich
delete from LichSuRutTien
update Carts set Status=0


update users set LevelThanhVien=0 where iuser_id=87
update users set LevelThanhVien=1 where iuser_id=26
update users set LevelThanhVien=2 where iuser_id=25
update users set LevelThanhVien=4 where iuser_id=24
update users set LevelThanhVien=1 where iuser_id=23
update users set LevelThanhVien=3 where iuser_id=15

select iuser_id,GioiThieu,LevelThanhVien from users  where iuser_id in (87,26,25,24,23,15)

select * from HoaHongThanhVien


***************************


SELECT (

select iuser_id  as thanhvien from users as tb
where tb.iuser_id=cc.iuser_id
and  ((MTree LIKE N'%|'+CONVERT(varchar,tb.iuser_id)+'|%'))

)as nguoinhan

,* FROM users as cc  


--------------------------------

delete from HoaHongThanhVien
delete from LichSuGiaoDich
delete from LichSuRutTien

delete from CapDiemThanhVien
delete from ChuyenDiemThanhVien

delete from Carts

delete from CartDetail

update users set ViTienHHGioiThieu=0,TongTienCoinDuocCap=0,VIAAFFILIATE=0,ViAgLang=0,TienDangSoHuuBatDongSan=0

update Carts set Status=0

update CartDetail set TrangThaiKhieuKien=0,TrangThaiNhaCungCap=3,TrangThaiNguoiMuaHang=3

// Khi Test chỉ cần thay đổi ID người mua là dc.

update CartDetail set IDThanhVien=9422
update Carts set IDThanhVien=9422


// Lệnh cộng tiền từ ví hỗ trợ sang ví AFF
UPDATE users SET VIAAFFILIATE = convert(float,(VIAAFFILIATE)) + convert(float,(ViTienHHGioiThieu)) 
select iuser_id,VIAAFFILIATE,ViTienHHGioiThieu from  users where ViTienHHGioiThieu!='0' Order by iuser_id asc
******** Kết thúc lệnh



SELECT TongTienCoinDuocCap FROM users
WHERE  CONVERT(float,TongTienCoinDuocCap) >1001