﻿#region Copyright and License
// Copyright 2010..2014 Alexander Reinert
// 
// This file is part of the ARSoft.Tools.Net - C# DNS client/server and SPF Library (http://arsofttoolsnet.codeplex.com/)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARSoft.Tools.Net.Dns
{
	/// <summary>
	///   <para>Canonical name for an alias</para>
	///   <para>
	///     Defined in
	///     <see cref="!:http://tools.ietf.org/html/rfc1035">RFC 1035</see>
	///   </para>
	/// </summary>
	public class CNameRecord : DnsRecordBase
	{
		/// <summary>
		///   Canonical name
		/// </summary>
		public string CanonicalName { get; private set; }

		internal CNameRecord() {}

		/// <summary>
		///   Creates a new instance of the CNameRecord class
		/// </summary>
		/// <param name="name"> Domain name the host </param>
		/// <param name="timeToLive"> Seconds the record should be cached at most </param>
		/// <param name="canonicalName"> Canocical name for the alias of the host </param>
		public CNameRecord(string name, int timeToLive, string canonicalName)
			: base(name, RecordType.CName, RecordClass.INet, timeToLive)
		{
			CanonicalName = canonicalName ?? String.Empty;
		}

		internal override void ParseRecordData(byte[] resultData, int startPosition, int length)
		{
			CanonicalName = DnsMessageBase.ParseDomainName(resultData, ref startPosition);
		}

		internal override string RecordDataToString()
		{
			return CanonicalName;
		}

		protected internal override int MaximumRecordDataLength
		{
			get { return CanonicalName.Length + 2; }
		}

		protected internal override void EncodeRecordData(byte[] messageData, int offset, ref int currentPosition, Dictionary<string, ushort> domainNames)
		{
			DnsMessageBase.EncodeDomainName(messageData, offset, ref currentPosition, CanonicalName, true, domainNames);
		}
	}
}