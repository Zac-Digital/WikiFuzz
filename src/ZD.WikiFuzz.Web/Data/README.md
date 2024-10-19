### Credit

1. https://en.wikipedia.org/wiki/Wikipedia:Database_download#English-language_Wikipedia

This provides instructions on how to download the Wikipedia dumps and utilise it.

2. https://dumps.wikimedia.org/enwiki/20241001/

I use this specific source, you will want to download the following:

1. enwiki-20241001-pages-articles-multistream.xml.bz2
2. enwiki-20241001-pages-articles-multistream-index.txt.bz2

---

## Instructions

1. Go to the link provided in the above credits, specifically number 2*.
2. Download the pair of files, one is `enwiki-<DATE>-pages-articles-multistream.xml.bz2`, the other is `enwiki-<DATE>-pages-articles-multistream-index.txt.bz2`
3. Extract `enwiki-<DATE>-pages-articles-multistream-index.txt.bz2`
4. Place `enwiki-<DATE>-pages-articles-multistream-index.txt` into the Data folder
5. Place `enwiki-<DATE>-pages-articles-multistream.xml.bz2` into the Data folder
6. Rename `enwiki-<DATE>-pages-articles-multistream-index.txt` to `Wiki-Index.txt`
7. Rename `enwiki-<DATE>-pages-articles-multistream.xml.bz2` to `Wiki-Data.bz2`

\* The date on this is the 1st of October 2024. They seem to do a monthly dump, so you may wish to always get the latest one by changing the URL to represent the month and year you're in. The 1st link also shows you what the latest one is.