﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Identity.Client;
using StreamingPlatform.Domain.Contract;
using StreamingPlatform.Domain.Models;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net;

namespace StreamingPlatform.Domain
{
    public class PlaylistService
    {
        private readonly StreamingPlatformContext _context;

        public PlaylistService(StreamingPlatformContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 建立播放清單
        /// </summary>
        /// <param name="name"></param>
        /// <param name="songIds"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool CreatePlaylist(string name, int[] songIds, int memberId)
        {
            var member = _context.Member
                                 .Include(m => m.PlayList)
                                 .FirstOrDefault(m => m.Id == memberId);

            if (member == null)
                throw new ArgumentNullException(nameof(memberId), "該會員不存在");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "需填寫播放清單名稱");

            if (songIds == null || !songIds.Any())
                throw new ArgumentNullException(nameof(songIds), "需指定歌曲");

            if (_context.Song.Where(m => songIds.Contains(m.Id)).Count() != songIds.Length)
                throw new ArgumentOutOfRangeException(nameof(songIds), "請指定有效的歌曲");

            var playlist = new PlayList
            {
                Name = name
            };

            foreach (var songId in songIds)
            {
                playlist.PlaylistItem.Add(new PlaylistItem
                {
                    SongId = songId
                });
            }

            member.PlayList.Add(playlist);

            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 刪除播放清單
        /// </summary>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        public bool DeletePlaylist(int playlistId)
        {
            var playlist = _context.PlayList
                                    .Include(m => m.PlaylistItem)
                                    .FirstOrDefault(m => m.Id == playlistId);

            if (playlist == null)
                return true;

            _context.PlayList.Remove(playlist);
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 編輯播放清單
        /// </summary>
        /// <param name="name"></param>
        /// <param name="songIds"></param>
        /// <param name="playlistId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool EditPlaylist(string name, int[] songIds, int playlistId)
        {
            var playlist = _context.PlayList
                                   .Include(m => m.PlaylistItem)
                                   .FirstOrDefault(m => m.Id == playlistId);

            if (playlist == null)
                throw new ArgumentNullException(nameof(playlistId), "指定的播放清單不存在，無法更新");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "需填寫播放清單名稱");

            if (songIds == null || !songIds.Any())
                throw new ArgumentNullException(nameof(songIds), "需指定歌曲");

            if (_context.Song.Where(m => songIds.Contains(m.Id)).Count() != songIds.Length)
                throw new ArgumentOutOfRangeException(nameof(songIds), "請指定有效的歌曲");

            playlist.Name = name;

            //新增歌曲
            foreach (var songId in songIds)
            {
                if (playlist.PlaylistItem.Any(m => m.SongId == songId))
                    continue;

                playlist.PlaylistItem.Add(new PlaylistItem
                {
                    SongId = songId
                });
            }

            //刪除歌曲
            List<int> deleteSongIds = new List<int>();

            foreach (var item in playlist.PlaylistItem)
            {
                if (songIds.Contains(item.SongId))
                    continue;

                deleteSongIds.Add(item.SongId);
            }

            foreach (var deleteSongId in deleteSongIds)
            {
                var deleteItem = playlist.PlaylistItem.FirstOrDefault(m => m.SongId == deleteSongId);

                if (deleteItem == null)
                    continue;

                playlist.PlaylistItem.Remove(deleteItem);
            }

            int saveCount = _context.SaveChanges();

            return saveCount > 0 && playlist.PlaylistItem.Count == songIds.Length && playlist.Name == name;
        }

        /// <summary>
        /// 取得播放清單列表
        /// </summary>
        /// <param name="membarId"></param>
        /// <param name="playlistName"></param>
        /// <returns></returns>
        public IEnumerable<PlaylistSearchDto> SearchPlaylist(int membarId, string playlistName)
        {
            var result = _context.PlayList.Where(m => m.MemberId == membarId);

            if (!string.IsNullOrEmpty(playlistName))
                result = result.Where(m => m.Name.Contains(playlistName));

            return result.AsNoTracking().Select(m => new PlaylistSearchDto
            {
                Id = m.Id,
                Name = m.Name,
                MemberId = m.MemberId
            });
        }
    }
}