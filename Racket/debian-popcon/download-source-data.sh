dataFolder=source
items=`echo http://popcon.debian.org/by_{inst,vote}.gz`

wget -N -P $dataFolder $items

for k in `ls $dataFolder/*.gz`
do
    gzip -dfk $k
done
